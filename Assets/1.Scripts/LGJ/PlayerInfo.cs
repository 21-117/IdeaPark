using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public static PlayerInfo localPlayer;

    // 현재 하이라이트 된 오브젝트
    public GameObject grabObject;
    public GameObject rayObject;
    public GameObject buttonMind;
    public FlexibleColorPicker fcp;
    public bool createBubble = false;
    public Material bubbleMat, lineMat;
    public GameObject palette, keyboard, ai, delete;

    public MeshRenderer head;
    public SkinnedMeshRenderer leftHand, rightHand;
    // 왼손, 오른손
    public GameObject left_Hand_Obj, right_Hand_Obj;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            localPlayer = this;
        }
        else if (photonView.IsMine == false)
        {
            //PlayerInfo 컴포넌트를 비활성화
            this.enabled = false;
        }
    }

    private void Start()
    {
        GrabObject = null;
        RayObject = null;
        XROrigin rig = FindObjectOfType<XROrigin>();
        buttonMind = rig.transform.Find("Camera Offset/Left Hand/UIs/Button_Mind").gameObject;

        fcp.transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Update()
    {
        lineMat.SetColor("Color_365294aa63504c3d8850a85d02e3fff8", bubbleMat.GetColor("Color_cf12b49411d94583a269f83e6981abd1"));
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

    // 구체 삭제
    public void DeleteBubble()
    {
        if (GrabObject != null)
        {
            GrabObject.GetComponent<XR_Bubble>().buttonMind.SetActive(false);
            GrabObject.SetActive(false);
        }
    }

    // 구체 만들기
    public void CreateBubble()
    {
        if (GrabObject != null)
        {

        }
    }


}
