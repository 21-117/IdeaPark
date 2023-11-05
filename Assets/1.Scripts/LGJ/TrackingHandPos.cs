using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingHandPos : MonoBehaviour
{
    public Transform hand;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = hand.position;
        this.transform.rotation = hand.rotation;
    }
}
