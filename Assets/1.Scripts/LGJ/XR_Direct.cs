using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Hands.XRHandSubsystemDescriptor;

public class XR_Direct : XRDirectInteractor
{
    [System.Obsolete]
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        Debug.Log("hover entered");
    }
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
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
