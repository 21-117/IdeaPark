using Nova;
using NovaSamples.UIControls;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_Bubble : XRGrabInteractable
{
    public GameObject buttonMind;
    private int toggleCount;
    private Toggle[] buttonToggles;
    public Text mindText;

    private PhotonView photonView;

    private void Awake()
    {
        base.Awake();
        mindText = this.transform.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        //buttonMind = PlayerInfo.localPlayer.buttonMind;
        //toggleCount = buttonMind.GetComponentsInChildren<Toggle>().Count();
        //buttonToggles = new Toggle[toggleCount];
        //// �� ��ũ��Ʈ�� ���� ���� ������Ʈ�� �ڽĵ� �� Toggle ������Ʈ�� ���� ������Ʈ���� ã��
        //buttonToggles = buttonMind.GetComponentsInChildren<Toggle>();
    }
    private void Update()
    {

        if (PlayerInfo.localPlayer != null)
        {
            buttonMind = PlayerInfo.localPlayer.buttonMind;
            toggleCount = buttonMind.GetComponentsInChildren<Toggle>().Count();
            buttonToggles = new Toggle[toggleCount];
            // �� ��ũ��Ʈ�� ���� ���� ������Ʈ�� �ڽĵ� �� Toggle ������Ʈ�� ���� ������Ʈ���� ã��
            buttonToggles = buttonMind.GetComponentsInChildren<Toggle>();
        }

    }

    [System.Obsolete]
    protected override void OnHoverExited(XRBaseInteractor interactor)
    {
        base.OnHoverExited(interactor);
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other == CheckFingerTip.instance.fingerCol && !CheckFingerTip.instance.touchTip)
    //    {
    //        if (!buttonMind.activeSelf)
    //        {
    //            buttonMind.SetActive(true);
    //            buttonMind.transform.position = this.transform.position;
    //        }
    //        else if (buttonMind.activeSelf)
    //        {
    //            buttonMind.SetActive(false);

    //            foreach (Toggle toggle in buttonToggles)
    //            {
    //                toggle.isOn = false;
    //            }
    //        }
    //    }
    //}

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        photonView.RequestOwnership();
        base.OnSelectEntered(interactor);
    }

    public void OffButtonMind()
    {
        if (buttonMind.activeSelf)
        {
            buttonMind.SetActive(false);

            foreach (Toggle toggle in buttonToggles)
            {
                toggle.isOn = false;
            }
        }
    }
}



