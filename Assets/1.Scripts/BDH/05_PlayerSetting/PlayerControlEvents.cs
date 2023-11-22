using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Oculus.VoiceSDK.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlEvents : DistanceGrabInteractor
{
    protected override void InteractableSelected(DistanceGrabInteractable interactable)
    {
        base.InteractableSelected(interactable);
        print(interactable.gameObject.name);
    }

    protected override void InteractableUnselected(DistanceGrabInteractable interactable)
    {
        base.InteractableUnselected(interactable);
        print(interactable.gameObject.name); 
    }

   



}
