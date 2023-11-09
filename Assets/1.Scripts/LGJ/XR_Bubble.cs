using Nova;
using NovaSamples.UIControls;
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

    private void Awake()
    {
        base.Awake();
        mindText = this.transform.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        //buttonMind = PlayerInfo.localPlayer.buttonMind;
        //toggleCount = buttonMind.GetComponentsInChildren<Toggle>().Count();
        //buttonToggles = new Toggle[toggleCount];
        //// 이 스크립트를 가진 게임 오브젝트의 자식들 중 Toggle 컴포넌트를 가진 오브젝트들을 찾기
        //buttonToggles = buttonMind.GetComponentsInChildren<Toggle>();
    }
    private void Update()
    {
        if (PlayerInfo.localPlayer != null)
        {
            buttonMind = PlayerInfo.localPlayer.buttonMind;
            toggleCount = buttonMind.GetComponentsInChildren<Toggle>().Count();
            buttonToggles = new Toggle[toggleCount];
            // 이 스크립트를 가진 게임 오브젝트의 자식들 중 Toggle 컴포넌트를 가진 오브젝트들을 찾기
            buttonToggles = buttonMind.GetComponentsInChildren<Toggle>();
        }
    }

    [System.Obsolete]
    protected override void OnHoverExited(XRBaseInteractor interactor)
    {
        base.OnHoverExited(interactor);

        //Debug.Log(interactor.gameObject.name);

        //if (!buttonMind.activeSelf && interactor.TryGetComponent(out CheckFingerTip checkFingerTip))
        //{
        //    buttonMind.SetActive(true);
        //    buttonMind.transform.position = this.transform.position;
        //}
        //else if (buttonMind.activeSelf && interactor.TryGetComponent(out CheckFingerTip checkFingerTip2))
        //{
        //    buttonMind.SetActive(false);

        //    foreach (Toggle toggle in buttonToggles)
        //    {
        //        toggle.isOn = false;
        //    }
        //}

        if (interactor.TryGetComponent(out XR_Direct dir))
        {
            MindMapController.state = MindMapController.State.CREATE;
        }
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



