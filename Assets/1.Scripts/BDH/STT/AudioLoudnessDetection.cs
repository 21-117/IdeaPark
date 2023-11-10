using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;

    private void Start()
    {
        MicrophoneToAudioClip();
    }

    // 마이크를 이용하는 메소드. 
    public void MicrophoneToAudioClip()
    {
        // 마이크를 원하는 것으로 설정 가능하다. 
        //string microphoneName = Microphone.devices[0]; // 0번 오큘러스 해드셋 
        string microphoneName = Microphone.devices[3];
        print(microphoneName);
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);

        //microphoneClip = Microphone.Start(microphoneName, false, 60, 16000);
    }

    // 마이크 장치 실행 시 Loudness 측정 
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[3]), microphoneClip);
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

