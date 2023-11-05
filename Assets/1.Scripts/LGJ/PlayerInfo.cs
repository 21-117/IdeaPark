using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    // ���� ���̶���Ʈ �� ������Ʈ
    public GameObject cursorObject;
    public GameObject rayObject;
    public GameObject buttonMind;
    public FlexibleColorPicker fcp;
    public bool createBubble = false;
    // �޼�, ������
    public GameObject left_Hand_Obj, right_Hand_Obj;
    private void Awake()
    {
        if(instance != null) Destroy(instance);
        instance = this;
    }

    // ��ü ����
    public void DeleteBubble()
    {
        if(cursorObject != null)
        {
            cursorObject.GetComponent<XR_Bubble>().buttonMind.SetActive(false);
            cursorObject.SetActive(false);
        }
    }

    // ��ü �����
    public void CreateBubble()
    {
        if(cursorObject != null)
        {

        }
    }


}
