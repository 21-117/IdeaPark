using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Hands.XRHandSubsystemDescriptor;

public class XR_Direct : XRDirectInteractor
{
    public GameObject buttonMind;

    [System.Obsolete]
    protected override void OnHoverEntered(XRBaseInteractable interactable)
    {
        base.OnHoverEntered(interactable);
        Debug.Log("hover entered");
    }
    protected override void OnHoverExited(XRBaseInteractable interactable)
    {
        base.OnHoverExited(interactable);
        Debug.Log("hover exited");

       
    }

    [System.Obsolete]
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        Debug.Log("select entered");
    }
    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        Debug.Log("select exited");
    }
}
