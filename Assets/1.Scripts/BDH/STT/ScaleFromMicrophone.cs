using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    public AudioLoudnessDetection detector;
    public AudioSource source;
    public Vector3 minScale;
    public Vector3 maxScale;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    private void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness < threshold)
            loudness = 0;

        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
