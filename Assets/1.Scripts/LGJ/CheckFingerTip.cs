using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFingerTip : MonoBehaviour
{
    public static CheckFingerTip instance;
    public Collider fingerCol;
    public bool touchTip;
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;

        fingerCol = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            touchTip = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            touchTip = false;
        }
    }
}
