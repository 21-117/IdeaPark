using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class IdeaParkManager : MonoBehaviour
{
    public Material skyMat;
    private float skyRotSpeed;

    void Start()
    {
        skyRotSpeed = 0.1f;
    }

    void Update()
    {
        skyMat.SetFloat("_Rotation", Time.time * skyRotSpeed);
    }
}
