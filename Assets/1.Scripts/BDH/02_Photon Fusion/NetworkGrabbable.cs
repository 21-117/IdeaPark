using Fusion;
using Oculus.Interaction;
using System;
using UnityEngine;

public class NetworkGrabbable : NetworkBehaviour, IStateAuthorityChanged
{
    private Grabbable grabbable;
    private Rigidbody rb;

    [Networked(OnChanged = nameof(OnIsKinematicChanged))] NetworkBool IsKinematic {  get; set; }

    private bool isGrabbed; 

    public override void Spawned()
    {
        grabbable = GetComponent<Grabbable>();
        rb = GetComponent<Rigidbody>();

        grabbable.WhenPointerEventRaised += Grabbable_WhenPointerEventRaised; 
    }

    private void Grabbable_WhenPointerEventRaised(PointerEvent events)
    {
        if(events.Type == PointerEventType.Select)
        {
            if (Object.HasStateAuthority) OneGrab();
            else Object.ReleaseStateAuthority();
        }
        else if(events.Type == PointerEventType.Unselect)
            if(Object.HasStateAuthority && !isGrabbed) IsKinematic = false;
    }

    private static void OnIsKinematicChanged(Changed<NetworkGrabbable> changed) => changed.Behaviour.ToggleIsKinematic();

    private void ToggleIsKinematic()
    {
        rb.isKinematic = IsKinematic;
        rb.useGravity = !IsKinematic;
    }

    public void StateAuthorityChanged()
    {
        if (isGrabbed && !Object.HasStateAuthority)
        {
            isGrabbed = false;

            grabbable.enabled = false;
            grabbable.enabled = true;
        }
        else if (Object.HasStateAuthority) OneGrab();
    }

    private void OneGrab()
    {
        isGrabbed = true;
        IsKinematic = true; 
    }
}
