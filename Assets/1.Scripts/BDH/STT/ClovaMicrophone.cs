using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

// Clova STT로 부터 받은 데이터 값. 
[Serializable]
public class VoiceRecognize
{
    public string text;
}

public class ClovaMicrophone : MonoBehaviour
{
    // STT 기능 활성화 
    public static Action onStartSTT;
    // STT 기능 비활성화
    public static Action onStopSTT;

    private float loudnessSensibility = 100f;
    private float threshold = 0.1f;
    private float loudness; 
    private float recordingTimeOut = 10f;
    private bool isRecording = false; 

    // 오디오 클립 저장. 
    private AudioClip clip;

    // 오디오 디바이스 배열
    string[] micList;

    // 오디오 디바이스 정수 
    private const int MicDevices = 0;// PC 오디오 스피커 3번, 오큘러스 헤드셋 0번, 

    // Clova API 정보 셋팅 
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
        // 1. Photon voice Recorder Transmit Enabled를 비활성화 .
        VoiceRecorderController.onStopTransmit();

        print("네이버 API 호출을 위한 녹음을 시작합니다. 현재 마이크 환경 : " + micList[MicDevices]);

        // 3. 네이버 API 호출을 위한 녹음
        clip = Microphone.Start(micList[MicDevices], true, 10, 44100);
    }

    private void onStopRecoderMicrophone()
    {
        print("네이버 API 호출을 위한 녹음을 종료합니다. ");
        isRecording = false;

        // 네이버 API 호출을 위한 녹음을 종료
        Microphone.End(micList[MicDevices]);
    }

    private void onCallNaverAPI()
    {
        // 네이버 API 호출을 위한 녹음을 종료
        onStopRecoderMicrophone();

        // Photon voice Recorder Transmit Enabled를 활성화 .
        VoiceRecorderController.onStartTransmit();

        // 경로에 오디오 파일 WAV 저장. 
        SaveAudioClipToWAV(Application.dataPath + "/test.wav");

        // 오디오 경로 파일을 바이트 배열 파일로 변환. 
        byte[] byteData = File.ReadAllBytes(Application.dataPath + "/test.wav");

        // 네이버 STT API 코루틴 호출. 
        StartCoroutine(PostVoice(apiUrl, byteData));
    }

    // 마이크로폰 녹음 시작 코루틴. 
    // 예외처리 : 10초 이상 Microphone의 입력이 없는 경우 종료
    IEnumerator StartRecording()
    {
        float timer = 0f; 

        isRecording = true;

        // 0.2 초 잠시 대기한 후 
        yield return new WaitForSeconds(0.2f);

        // 녹음 시작 메소드 호출 
        onRecoderMicrophone();

        while (isRecording)
        {
            // 매 프레임마다 체크 
            yield return null;

            timer += Time.deltaTime;

            // 입력이 감지되면 타이머 초기화
            if (Microphone.IsRecording(micList[MicDevices]) && loudness > threshold)
            {          
                timer = 0f;
            }
           
            // 입력이 10초 이상 없으면 녹음 종료. 
            if (timer > recordingTimeOut)
            {
                // 녹음 종료 메소드 호출. 
                onStopRecoderMicrophone(); 
            }

        }
    }

    // Naver API로 오디오 데이터 전송. 
    private IEnumerator PostVoice(string url, byte[] data)
    {
        // request 생성
        WWWForm form = new WWWForm();
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // 요청 헤더 설정
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);
        request.SetRequestHeader("Content-Type", "application/octet-stream");

        // 바디에 처리과정을 거친 Audio Clip data를 실어줌
        request.uploadHandler = new UploadHandlerRaw(data);

        // 요청을 보낸 후 response를 받을 때까지 대기
        yield return request.SendWebRequest();

        // 만약 response가 비어있다면 error
        if (request == null)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // API 응답을 텍스트로 변환. 

            // json 형태로 받음 {"text":"인식결과"}
            string message = request.downloadHandler.text;
            print("서버에 요청한 결과 메시지 : " + message);
            VoiceRecognize voiceRecognize = JsonUtility.FromJson<VoiceRecognize>(message);

            Debug.Log("Voice Server responded: " + voiceRecognize.text);
            // Voice Server responded: 인식결과

            // 해당 마인드 맵에 인식한 텍스트 적용. 
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

        // AudioClip의 오디오 데이터 가져오기
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        // WAV 파일 헤더 작성
        using (FileStream fs = File.Create(filePath))
        {
            WriteWAVHeader(fs, clip.channels, clip.frequency, clip.samples);
            ConvertAndWrite(fs, samples);
        }

        Debug.Log("AudioClip saved as WAV: " + filePath);
    }

    // WAV 파일 헤더 작성
    private void WriteWAVHeader(FileStream fileStream, int channels, int frequency, int sampleCount)
    {
        var samples = sampleCount * channels;
        var fileSize = samples + 36;

        fileStream.Write(new byte[] { 82, 73, 70, 70 }, 0, 4); // "RIFF" 헤더
        fileStream.Write(BitConverter.GetBytes(fileSize), 0, 4);
        fileStream.Write(new byte[] { 87, 65, 86, 69 }, 0, 4); // "WAVE" 헤더
        fileStream.Write(new byte[] { 102, 109, 116, 32 }, 0, 4); // "fmt " 헤더
        fileStream.Write(BitConverter.GetBytes(16), 0, 4); // 16
        fileStream.Write(BitConverter.GetBytes(1), 0, 2); // 오디오 포맷 (PCM)
        fileStream.Write(BitConverter.GetBytes(channels), 0, 2); // 채널 수
        fileStream.Write(BitConverter.GetBytes(frequency), 0, 4); // 샘플 레이트
        fileStream.Write(BitConverter.GetBytes(frequency * channels * 2), 0, 4); // 바이트 레이트
        fileStream.Write(BitConverter.GetBytes(channels * 2), 0, 2); // 블록 크기
        fileStream.Write(BitConverter.GetBytes(16), 0, 2); // 비트 레이트
        fileStream.Write(new byte[] { 100, 97, 116, 97 }, 0, 4); // "data" 헤더
        fileStream.Write(BitConverter.GetBytes(samples), 0, 4);
    }

    // 오디오 데이터 변환 및 작성
    private void ConvertAndWrite(FileStream fileStream, float[] samples)
    {
        Int16[] intData = new Int16[samples.Length];
        // float -> Int16 변환

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * 32767);
        }

        // Int16 데이터 작성
        Byte[] bytesData = new Byte[intData.Length * 2];
        Buffer.BlockCopy(intData, 0, bytesData, 0, bytesData.Length);
        fileStream.Write(bytesData, 0, bytesData.Length);
    }

    // 마이크 장치 실행 시 Loudness 측정 
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(micList[MicDevices]), clip);
    }

    // 오디오 클립 실행 시 Loudness 측정 
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
