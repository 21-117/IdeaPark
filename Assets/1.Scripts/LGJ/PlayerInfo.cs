using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    // 현재 하이라이트 된 오브젝트
    public GameObject cursorObject;
    public GameObject rayObject;
    public GameObject buttonMind;
    public FlexibleColorPicker fcp;
    public bool createBubble = false;
    // 왼손, 오른손
    public GameObject left_Hand_Obj, right_Hand_Obj;
    private void Awake()
    {
        if(instance != null) Destroy(instance);
        instance = this;
    }

    // 구체 삭제
    public void DeleteBubble()
    {
        if(cursorObject != null)
        {
            cursorObject.GetComponent<XR_Bubble>().buttonMind.SetActive(false);
            cursorObject.SetActive(false);
        }
    }

    // 구체 만들기
    public void CreateBubble()
    {
        if(cursorObject != null)
        {

        }
    }


}
