using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    // ���� ���̶���Ʈ �� ������Ʈ
    public GameObject cursorObject;

    private void Awake()
    {
        if(instance != null) Destroy(instance);
        instance = this;
    }



}
