using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UIAudio;

public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum UISoundState
    {
        SFX_UI_CLICK,
        SFX_UI_COMPLETE,
        SFX_UI_CANCEL
    }

    public UISoundState soundState; 
    
    public void OnPointerClick(PointerEventData eventData)
    {

        switch (soundState)
        {
            case UISoundState.SFX_UI_CLICK:
                SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UIMENU_ONCLICK);
                break;
            case UISoundState.SFX_UI_COMPLETE:
                SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_COMPLETE);
                break;
            case UISoundState.SFX_UI_CANCEL:
                break;
            default:
                SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_CANCEL);
                break;
        }
        
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
       
    }
}
