using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

// Clova STT�� ���� ���� ������ ��. 
[Serializable]
public class VoiceRecognize
{
    public string text;
}

public class ClovaMicrophone : MonoBehaviour
{
    // STT ��� Ȱ��ȭ 
    public static Action onStartSTT;
    // STT ��� ��Ȱ��ȭ
    public static Action onStopSTT;

    private float loudnessSensibility = 100f;
    private float threshold = 0.1f;
    private float loudness; 
    private float recordingTimeOut = 10f;
    private bool isRecording = false; 

    // ����� Ŭ�� ����. 
    private AudioClip clip;

    // ����� ����̽� �迭
    string[] micList;

    // ����� ����̽� ���� 
    private const int MicDevices = 0;// PC ����� ����Ŀ 3��, ��ŧ���� ���� 0��, 

    // Clova API ���� ���� 
    private string apiUrl = "https://naveropenapi.apigw.ntruss.com/recog/v1/stt?lang=Kor";
    private string clientID = "zz92lidnq2";
    private string clientSecret = "yVjtEJb8vYSGk8koNTN3YP7MSSdiRFSNe45uGAu7";
    private int sampleWindow = 64;

    void Awake()
    {
        
        micList = Microphone.devices;
    }

    private void Start()
    {
        onStartSTT = () => { StartCoroutine(StartRecording()); };
        onStopSTT = () => { onCallNaverAPI(); };
    }

    private void Update()
    {
        float loudness = GetLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness < threshold)
            loudness = 0;


    }

    private void onRecoderMicrophone()
    {
        // 1. Photon voice Recorder Transmit Enabled�� ��Ȱ��ȭ .
        VoiceRecorderController.onStopTransmit();

        print("���̹� API ȣ���� ���� ������ �����մϴ�. ���� ����ũ ȯ�� : " + micList[MicDevices]);

        // 3. ���̹� API ȣ���� ���� ����
        clip = Microphone.Start(micList[MicDevices], true, 10, 44100);
    }

    private void onStopRecoderMicrophone()
    {
        print("���̹� API ȣ���� ���� ������ �����մϴ�. ");
        isRecording = false;

        // ���̹� API ȣ���� ���� ������ ����
        Microphone.End(micList[MicDevices]);
    }

    private void onCallNaverAPI()
    {
        // ���̹� API ȣ���� ���� ������ ����
        onStopRecoderMicrophone();

        // Photon voice Recorder Transmit Enabled�� Ȱ��ȭ .
        VoiceRecorderController.onStartTransmit();

        // ��ο� ����� ���� WAV ����. 
        SaveAudioClipToWAV(Application.dataPath + "/test.wav");

        // ����� ��� ������ ����Ʈ �迭 ���Ϸ� ��ȯ. 
        byte[] byteData = File.ReadAllBytes(Application.dataPath + "/test.wav");

        // ���̹� STT API �ڷ�ƾ ȣ��. 
        StartCoroutine(PostVoice(apiUrl, byteData));
    }

    // ����ũ���� ���� ���� �ڷ�ƾ. 
    // ����ó�� : 10�� �̻� Microphone�� �Է��� ���� ��� ����
    IEnumerator StartRecording()
    {
        float timer = 0f; 

        isRecording = true;

        // 0.2 �� ��� ����� �� 
        yield return new WaitForSeconds(0.2f);

        // ���� ���� �޼ҵ� ȣ�� 
        onRecoderMicrophone();

        while (isRecording)
        {
            // �� �����Ӹ��� üũ 
            yield return null;

            timer += Time.deltaTime;

            // �Է��� �����Ǹ� Ÿ�̸� �ʱ�ȭ
            if (Microphone.IsRecording(micList[MicDevices]) && loudness > threshold)
            {          
                timer = 0f;
            }
           
            // �Է��� 10�� �̻� ������ ���� ����. 
            if (timer > recordingTimeOut)
            {
                // ���� ���� �޼ҵ� ȣ��. 
                onStopRecoderMicrophone(); 
            }

        }
    }

    // Naver API�� ����� ������ ����. 
    private IEnumerator PostVoice(string url, byte[] data)
    {
        // request ����
        WWWForm form = new WWWForm();
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // ��û ��� ����
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);
        request.SetRequestHeader("Content-Type", "application/octet-stream");

        // �ٵ� ó�������� ��ģ Audio Clip data�� �Ǿ���
        request.uploadHandler = new UploadHandlerRaw(data);

        // ��û�� ���� �� response�� ���� ������ ���
        yield return request.SendWebRequest();

        // ���� response�� ����ִٸ� error
        if (request == null)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // API ������ �ؽ�Ʈ�� ��ȯ. 

            // json ���·� ���� {"text":"�νİ��"}
            string message = request.downloadHandler.text;
            print("������ ��û�� ��� �޽��� : " + message);
            VoiceRecognize voiceRecognize = JsonUtility.FromJson<VoiceRecognize>(message);

            Debug.Log("Voice Server responded: " + voiceRecognize.text);
            // Voice Server responded: �νİ��

            // �ش� ���ε� �ʿ� �ν��� �ؽ�Ʈ ����. 
            PlayerInfo.localPlayer.grabObject.GetComponentInChildren<Text>().text = voiceRecognize.text;
        }
    }

    public void SaveAudioClipToWAV(string filePath)
    {
        if (clip == null)
        {
            Debug.LogError("No AudioClip assigned.");
            return;
        }

        // AudioClip�� ����� ������ ��������
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        // WAV ���� ��� �ۼ�
        using (FileStream fs = File.Create(filePath))
        {
            WriteWAVHeader(fs, clip.channels, clip.frequency, clip.samples);
            ConvertAndWrite(fs, samples);
        }

        Debug.Log("AudioClip saved as WAV: " + filePath);
    }

    // WAV ���� ��� �ۼ�
    private void WriteWAVHeader(FileStream fileStream, int channels, int frequency, int sampleCount)
    {
        var samples = sampleCount * channels;
        var fileSize = samples + 36;

        fileStream.Write(new byte[] { 82, 73, 70, 70 }, 0, 4); // "RIFF" ���
        fileStream.Write(BitConverter.GetBytes(fileSize), 0, 4);
        fileStream.Write(new byte[] { 87, 65, 86, 69 }, 0, 4); // "WAVE" ���
        fileStream.Write(new byte[] { 102, 109, 116, 32 }, 0, 4); // "fmt " ���
        fileStream.Write(BitConverter.GetBytes(16), 0, 4); // 16
        fileStream.Write(BitConverter.GetBytes(1), 0, 2); // ����� ���� (PCM)
        fileStream.Write(BitConverter.GetBytes(channels), 0, 2); // ä�� ��
        fileStream.Write(BitConverter.GetBytes(frequency), 0, 4); // ���� ����Ʈ
        fileStream.Write(BitConverter.GetBytes(frequency * channels * 2), 0, 4); // ����Ʈ ����Ʈ
        fileStream.Write(BitConverter.GetBytes(channels * 2), 0, 2); // ��� ũ��
        fileStream.Write(BitConverter.GetBytes(16), 0, 2); // ��Ʈ ����Ʈ
        fileStream.Write(new byte[] { 100, 97, 116, 97 }, 0, 4); // "data" ���
        fileStream.Write(BitConverter.GetBytes(samples), 0, 4);
    }

    // ����� ������ ��ȯ �� �ۼ�
    private void ConvertAndWrite(FileStream fileStream, float[] samples)
    {
        Int16[] intData = new Int16[samples.Length];
        // float -> Int16 ��ȯ

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * 32767);
        }

        // Int16 ������ �ۼ�
        Byte[] bytesData = new Byte[intData.Length * 2];
        Buffer.BlockCopy(intData, 0, bytesData, 0, bytesData.Length);
        fileStream.Write(bytesData, 0, bytesData.Length);
    }

    // ����ũ ��ġ ���� �� Loudness ���� 
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(micList[MicDevices]), clip);
    }

    // ����� Ŭ�� ���� �� Loudness ���� 
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0f;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }


}
