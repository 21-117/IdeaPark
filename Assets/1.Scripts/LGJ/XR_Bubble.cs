using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_Bubble : XRGrabInteractable
{
    public GameObject buttonMind;

    [System.Obsolete]
    protected override void OnHoverExited(XRBaseInteractor interactor)
    {
        base.OnHoverExited(interactor);

        if (!buttonMind.activeSelf && interactor.TryGetComponent(out XR_Direct xR_Direct))//)
        {
            buttonMind.SetActive(true);
            buttonMind.transform.position = this.transform.position;
        }
        else
        {
            if (buttonMind.activeSelf && interactor.TryGetComponent(out XR_Direct xr))
            {
                buttonMind.SetActive(false);
            }
        }

    }

    
}
