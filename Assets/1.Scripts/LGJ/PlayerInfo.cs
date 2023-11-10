using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public static PlayerInfo localPlayer;

    // ���� ���̶���Ʈ �� ������Ʈ
    public GameObject grabObject;
    public GameObject rayObject;
    public GameObject buttonMind;
    public FlexibleColorPicker fcp;
    public bool createBubble = false;

    // �޼�, ������
    public GameObject left_Hand_Obj, right_Hand_Obj;
    private void Awake()
    {
        if (photonView.IsMine)
        {
            localPlayer = this;
        }
        else if (photonView.IsMine == false)
        {
            //PlayerInfo ������Ʈ�� ��Ȱ��ȭ
            this.enabled = false;
        }
    }

    private void Start()
    {
        GrabObject = null;
        RayObject = null;

    }

    public GameObject GrabObject
    {
        get { return grabObject; }
        set { grabObject = value; }
    }

    public GameObject RayObject
    {
        get { return rayObject; }
        set { rayObject = value; }
    }

    // ��ü ����
    public void DeleteBubble()
    {
        if (GrabObject != null)
        {
            GrabObject.GetComponent<XR_Bubble>().buttonMind.SetActive(false);
            GrabObject.SetActive(false);
        }
    }

    // ��ü �����
    public void CreateBubble()
    {
        if (GrabObject != null)
        {

        }
    }


}
