using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    // 현재 하이라이트 된 오브젝트
    public GameObject cursorObject;

    private void Awake()
    {
        if(instance != null) Destroy(instance);
        instance = this;
    }



}
