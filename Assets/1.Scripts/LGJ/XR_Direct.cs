using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Hands.XRHandSubsystemDescriptor;

public class XR_Direct : XRDirectInteractor
{
    public Transform indexTip;


    [System.Obsolete]
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        MindMapController.setHover(true, args.interactableObject.transform.gameObject);
        Debug.Log("hover entered");
    }
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        MindMapController.setHover(false, null);
        Debug.Log("hover exited");
    }


    [System.Obsolete]
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        // 2�� �̻��� �����ϸ� ���ε� ��带 ������ �� �ִ�. 
     
        Debug.Log("select entered");
    }
    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        MindMapController.setPinch(true, args.interactableObject.transform.gameObject);
        MindMapController.state = MindMapController.State.CONNECTION;
       
        Debug.Log("select exited");
    }
}
