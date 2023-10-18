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
    public int barCode;
    public GameObject getParent;
}
