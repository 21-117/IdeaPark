using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_Ray : XRRayInteractor
{
    public LayerMask layerMask;
    void Update()
    {
        MindMapController.instance.UpdateConnection();
    }

    [System.Obsolete]
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        //MindMapController.state = MindMapController.State.CONNECTION;
        Debug.Log("select entered");
        if (args.interactableObject.transform.TryGetComponent(out XR_Bubble bubble))
        {
            PlayerInfo.instance.rayObject = bubble.gameObject;
            bubble.trackPosition = false;
        }
    }
    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        //MindMapController.state = MindMapController.State.CREATE;

        if (MindMapController.instance._nodeAttach == false && MindMapController.instance._nodeDetach == false)
        {
            MindMapController.instance._nodeAttach = true;
        }
        else if (MindMapController.instance._nodeAttach == true && MindMapController.instance._nodeDetach == false)
        {
            MindMapController.instance._nodeAttach = false;
            MindMapController.instance._nodeDetach = true;
        }
        else if (MindMapController.instance._nodeAttach == false && MindMapController.instance._nodeDetach == true)
        {
            MindMapController.instance._nodeAttach = true;
            MindMapController.instance._nodeDetach = false;
        }


        if (args.interactableObject.transform.TryGetComponent(out XR_Bubble bubble))
        {
            PlayerInfo.instance.rayObject = null;
            bubble.trackPosition = true;
        }
        //MindMapController.setPinch(true, args.interactableObject.transform.gameObject);
        //MindMapController.state = MindMapController.State.CONNECTION;
        //MindMapController.instance.UpdateConnection();
        Debug.Log("select exited");
    }
}
