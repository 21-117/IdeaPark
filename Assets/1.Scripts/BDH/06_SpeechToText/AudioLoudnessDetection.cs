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

    // ����ũ�� �̿��ϴ� �޼ҵ�. 
    public void MicrophoneToAudioClip()
    {
        // ����ũ�� ���ϴ� ������ ���� �����ϴ�. 
        string microphoneName = Microphone.devices[0]; // 0�� ��ŧ���� �ص�� 

        print(microphoneName);
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);

    }

    // ����ũ ��ġ ���� �� Loudness ���� 
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
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

