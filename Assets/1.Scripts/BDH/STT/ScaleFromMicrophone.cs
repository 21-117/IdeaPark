using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    private GameObject detector;
    private ClovaMicrophone microphone;
    public AudioSource audioSource;
    public Vector3 minScale;
    public Vector3 maxScale;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    private void Start()
    {
        detector = GameObject.Find("[ VOICE MANAGER ]");
        audioSource = GetComponent<AudioSource>();
        microphone = detector.transform.GetComponent<ClovaMicrophone>();
    }

    private void Update()
    {
        if(PlayerInfo.localPlayer.grabObject != null)
        {
            float loudness = microphone.GetLoudnessFromMicrophone() * loudnessSensibility;

            if (loudness < threshold)
                loudness = 0;

            PlayerInfo.localPlayer.grabObject.transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
        }
        
    }
}
