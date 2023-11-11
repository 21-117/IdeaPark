using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_Ray : XRRayInteractor
{
    public LayerMask layerMask;
    void Update()
    {
        if (PlayerInfo.localPlayer != null)
        {
            MindMapController.mindMapController.UpdateConnection();
        }
    }

    [System.Obsolete]
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        Debug.Log("select entered");
        if (args.interactableObject.transform.TryGetComponent(out XR_Bubble bubble))
        {
            PlayerInfo.localPlayer.RayObject = bubble.gameObject;
            bubble.trackPosition = false;
        }
    }
    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        if (MindMapController.mindMapController._nodeAttach == false && MindMapController.mindMapController._nodeDetach == false)
        {
            MindMapController.mindMapController._nodeAttach = true;
        }
        else if (MindMapController.mindMapController._nodeAttach == true && MindMapController.mindMapController._nodeDetach == false)
        {
            MindMapController.mindMapController._nodeAttach = false;
            MindMapController.mindMapController._nodeDetach = true;
        }
        else if (MindMapController.mindMapController._nodeAttach == false && MindMapController.mindMapController._nodeDetach == true)
        {
            MindMapController.mindMapController._nodeAttach = true;
            MindMapController.mindMapController._nodeDetach = false;
        }


        if (args.interactableObject.transform.TryGetComponent(out XR_Bubble bubble))
        {
            PlayerInfo.localPlayer.RayObject = null;
            bubble.trackPosition = true;
        }
        //MindMapController.setPinch(true, args.interactableObject.transform.gameObject);
        //MindMapController.state = MindMapController.State.CONNECTION;
        //MindMapController.instance.UpdateConnection();
        Debug.Log("select exited");
    }
}
