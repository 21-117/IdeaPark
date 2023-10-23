using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFingerTip : MonoBehaviour
{
    public static CheckFingerTip instance;
    public bool touchTip;
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    
    }
    private void OnTriggerEnter(Collider other)
    {
        touchTip = true;
    }

    private void OnTriggerExit(Collider other)
    {
        touchTip = false;
    }
}
