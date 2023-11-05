using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Hands.XRHandSubsystemDescriptor;
using System;

public enum HandPos
{
    Left,
    Right
}

public class XR_Direct : XRDirectInteractor
{
    public HandPos handpos;
    public Transform fingerTip, tumbTip;
    public LayerMask layerMask;
    public float radius = 0.5f;
    public GameObject sampleBubble, nodemanager;
    private Collider[] cols;
    private float createTime = 2.0f, curTime = 0f, lerpValue, touchTime = 0f, touchMaxTime = 2.0f;

    void Update()
    {
        //touchTime += Time.deltaTime;
        //if (touchMaxTime > touchTime)
        //{
        //    PlayerInfo.instance.buttonMind.SetActive(false);
        //}

        cols = Physics.OverlapSphere(this.transform.position, radius, layerMask);
        if (cols.Length == 0 && CheckFingerTip.instance.touchTip && PlayerInfo.instance.left_Hand_Obj == null && PlayerInfo.instance.right_Hand_Obj == null)
        {
            lerpValue = Mathf.Clamp01(curTime / createTime);
            if (sampleBubble != null)
            {
                sampleBubble.SetActive(true);
                sampleBubble.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, lerpValue);
            }
            curTime += Time.deltaTime;
            if (curTime > createTime && !PlayerInfo.instance.createBubble)
            {
                // 노드를 만들어도 되는 허용치 부여
                PlayerInfo.instance.createBubble = true;
                curTime = 0f;
                //touchTime = 0f;
            }
        }
        else
        {
            if (sampleBubble != null) sampleBubble.SetActive(false);
            curTime = 0f;
        }
    }

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
        //MindMapController.setHover(false, null);
        if (args.interactableObject.transform.GetComponent<InstanceID>() != null && args.interactableObject.transform.GetComponent<InstanceID>().curObjectType == ObjectType.Circle)
        {
            PlayerInfo.instance.cursorObject = args.interactableObject.transform.gameObject;
        }

        Debug.Log("hover exited");
    }


    [System.Obsolete]
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Debug.Log("select entered");

        if (args.interactableObject != null)
        {
            if(handpos == HandPos.Left)
            {
                PlayerInfo.instance.left_Hand_Obj = args.interactableObject.transform.gameObject; 
            }
            else if(handpos == HandPos.Right)
            {
                PlayerInfo.instance.right_Hand_Obj = args.interactableObject.transform.gameObject;
            }
        }
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        touchTime = 0f;
        if (args.interactableObject == null)
        {
            if (handpos == HandPos.Left)
            {
                PlayerInfo.instance.left_Hand_Obj = null;
            }
            else if (handpos == HandPos.Right)
            {
                PlayerInfo.instance.right_Hand_Obj = null;
            }
        }
        //MindMapController.setPinch(true, args.interactableObject.transform.gameObject);
        //MindMapController.state = MindMapController.State.CONNECTION;
        Debug.Log("select exited");
    }

}
