using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Hands.XRHandSubsystemDescriptor;

public class XR_Direct : XRDirectInteractor
{
    public Transform fingerTip, tumbTip;
    public LayerMask layerMask;
    public float radius = 0.5f;
    public GameObject sampleBubble;
    private Collider[] cols;
    private float createTime = 2.0f, curTime = 0f, lerpValue;

    //void Update()
    //{
    //    cols = Physics.OverlapSphere(this.transform.position, radius, layerMask);
    //    if(cols.Length == 0 && CheckFingerTip.instance.touchTip)
    //    {
    //        sampleBubble.SetActive(true);
    //        lerpValue = Mathf.Clamp01(curTime / createTime);
    //        sampleBubble.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, lerpValue);
    //        curTime += Time.deltaTime;
    //        if (curTime > createTime)
    //        {
    //            GameObject obj = Resources.Load<GameObject>("Bubble");
    //            GameObject node = Instantiate(obj, fingerTip.position, Quaternion.identity);
    //            curTime = 0f;
    //        }
    //    }
    //    else
    //    {
    //        sampleBubble.SetActive(false);
    //        curTime = 0f;
    //    }
    //}

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
       

        //if (args.interactableObject.transform.GetComponent<InstanceID>().curObjectType == ObjectType.Circle)
        //{
        //    PlayerInfo.instance.cursorObject = args.interactableObject.transform.gameObject;
        //}

        Debug.Log("hover exited");
    }


    [System.Obsolete]
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        // 2초 이상을 유지하면 마인드 노드를 생성할 수 있다. 
        Debug.Log("select entered");
    }
    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        //MindMapController.setPinch(true, args.interactableObject.transform.gameObject);
        //MindMapController.state = MindMapController.State.CONNECTION;
        Debug.Log("select exited");
    }
}
