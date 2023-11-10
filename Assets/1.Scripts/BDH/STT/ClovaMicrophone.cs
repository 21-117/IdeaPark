using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


// Clova STT�� ���� ���� ������ ��. 
[Serializable]
public class VoiceRecognize
{
    public string text;
}


public class ClovaMicrophone : MonoBehaviour
{
    // Clova API ���� ���� 
    private string apiUrl = "https://naveropenapi.apigw.ntruss.com/recog/v1/stt?lang=Kor";
    private string clientID = "zz92lidnq2";
    private string clientSecret = "yVjtEJb8vYSGk8koNTN3YP7MSSdiRFSNe45uGAu7";

    private string _microphoneID = null;
    private AudioClip _recording = null;
    private int _recordingLengthSec = 15;
    private int _recordingHZ = 22050;


    private AudioClip clip;

    string[] micList;
    void Awake()
    {
        micList = Microphone.devices;
    }

    private void Start()
    {
        // pc ����� ����Ŀ 3��,
        // ��ŧ���� ���� 0��, 
        _microphoneID = Microphone.devices[3];
    }

    // STT ȣ�� ����. -> ���� ���� 
    public void startRecording()
    {
        Debug.Log("start recording");
        _recording = Microphone.Start(_microphoneID, false, _recordingLengthSec, _recordingHZ);
        //_recording = Microphone.Start(_microphoneID, true, 20, AudioSettings.outputSampleRate);

    }

    // STT ȣ�� ���� -> ���� ���� 
    public void stopRecording()
    {
        if (Microphone.IsRecording(_microphoneID))
        {
            Microphone.End(_microphoneID);

            Debug.Log("stop recording");
            if (_recording == null)
            {
                Debug.LogError("nothing recorded");
                return;
            }

           // byte[] byteData = Convert(_recording);

            // ������ audioclip api ������ ����
           // StartCoroutine(PostVoice(apiUrl, byteData));
        }
        return;
    }

    //public byte[] Convert(AudioClip clip)
    //{
    //    var samples = new float[clip.samples];
    //    clip.GetData(samples, 0);

    //    MemoryStream stream = new MemoryStream();
    //    BinaryWriter writer = new BinaryWriter(stream);

    //    int length = samples.Length;
    //    writer.Write(length);

    //    foreach (var sample in samples)
    //    {
    //        writer.Write(sample);
    //    }

    //    return stream.ToArray();
    //}


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
        }
    }


   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            clip = Microphone.Start(micList[3], true, 10, 44100);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Microphone.End(micList[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SaveAudioClipToWAV(Application.dataPath + "/test.wav");

            byte[] byteData = File.ReadAllBytes(Application.dataPath + "/test.wav");
            StartCoroutine(PostVoice(apiUrl, byteData));
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


}
