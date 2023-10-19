using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Circle
}

public class InstanceID : MonoBehaviour
{
    public ObjectType curObjectType;

    public void FindLog()
    {
        Debug.Log("gg");
    }
}
