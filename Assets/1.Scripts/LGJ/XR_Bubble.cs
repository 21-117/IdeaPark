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

    private void Start()
    {
        buttonMind = PlayerInfo.instance.buttonMind;
        toggleCount = buttonMind.GetComponentsInChildren<Toggle>().Count();
        buttonToggles = new Toggle[toggleCount];
        // �� ��ũ��Ʈ�� ���� ���� ������Ʈ�� �ڽĵ� �� Toggle ������Ʈ�� ���� ������Ʈ���� ã��
        buttonToggles = buttonMind.GetComponentsInChildren<Toggle>();

    }

    private void OnTriggerExit(Collider other)
    {
        if (other == CheckFingerTip.instance.fingerCol)
        {
            if (!buttonMind.activeSelf)
            {
                buttonMind.SetActive(true);
                buttonMind.transform.position = this.transform.position;
            }
            else if (buttonMind.activeSelf)
            {
                buttonMind.SetActive(false);

                foreach (Toggle toggle in buttonToggles)
                {
                    toggle.isOn = false;
                }
            }
        }
    }
}

    //[System.Obsolete]
    //protected override void OnHoverExited(XRBaseInteractor interactor)
    //{
    //    base.OnHoverExited(interactor);

    //    Debug.Log(interactor.gameObject.name);
    //    if (!buttonMind.activeSelf && interactor.TryGetComponent(out CheckFingerTip checkFingerTip))
    //    {
    //        buttonMind.SetActive(true);
    //        buttonMind.transform.position = this.transform.position;
    //    }
    //    else
    //    {
    //        if (buttonMind.activeSelf && interactor.TryGetComponent(out CheckFingerTip checkFingerTip2))
    //        {
    //            buttonMind.SetActive(false);

    //            foreach (Toggle toggle in buttonToggles)
    //            {
    //                toggle.isOn = false;
    //            }
    //        }
    //    }
    //}

