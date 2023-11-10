using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


// Clova STT로 부터 받은 데이터 값. 
[Serializable]
public class VoiceRecognize
{
    public string text;
}


public class ClovaMicrophone : MonoBehaviour
{
    // Clova API 정보 셋팅 
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
        // pc 오디오 스피커 3번,
        // 오큘러스 헤드셋 0번, 
        _microphoneID = Microphone.devices[3];
    }

    // STT 호출 시작. -> 녹음 시작 
    public void startRecording()
    {
        Debug.Log("start recording");
        _recording = Microphone.Start(_microphoneID, false, _recordingLengthSec, _recordingHZ);
        //_recording = Microphone.Start(_microphoneID, true, 20, AudioSettings.outputSampleRate);

    }

    // STT 호출 종료 -> 녹음 종료 
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

            // 녹음된 audioclip api 서버로 보냄
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


}
