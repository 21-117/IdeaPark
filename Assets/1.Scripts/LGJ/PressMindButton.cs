using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.UI; 
using static System.Net.WebRequestMethods;

public enum PressButton
{
    none,
    ai,
    palette,
    keyboard,
    delete,
    voice,
    stt
}
public class PressMindButton : MonoBehaviour
{
    public PressButton state;
    [HideInInspector]
    public GameObject ai;
    [HideInInspector]
    public GameObject palette;
    [HideInInspector]
    public GameObject keyboard;

    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();    
    }

    private void Update()
    {
        if (PlayerInfo.localPlayer != null)
        {
            ai = PlayerInfo.localPlayer.ai;
            palette = PlayerInfo.localPlayer.palette;
            keyboard = PlayerInfo.localPlayer.keyboard;
        }
    }

   

    public void OnOffButton()
    {
        switch(state)
        {
            case PressButton.ai:             
                ButtonSetting(toggle.isOn, ai); 
                break;
            case PressButton.palette:
                ButtonSetting(toggle.isOn, palette);
                break;
            case PressButton.keyboard:
                ButtonSetting(toggle.isOn, keyboard);
                break;
            case PressButton.delete:
                CallbackMethod(toggle.isOn, PressButton.delete);
                break;
            case PressButton.voice:
                CallbackMethod(toggle.isOn, PressButton.voice);
                break;
            case PressButton.stt:
                CallbackMethod(toggle.isOn, PressButton.stt);
                break; 

        }

    }

    private void CallbackMethod(bool IsToggle, PressButton state)
    {
        if (IsToggle)
        {
            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_COMPLETE);

            switch (state)
            {
                case PressButton.delete:
                    // 삭제할 메소드 호출.
                    break;
                case PressButton.voice:
                    // 포톤 보이스 활성화 메소드 호출.
                    VoiceRecorderController.onStartTransmit();
                    break;
                case PressButton.stt:
                    // stt 녹음 활성화 메소드 호출. 
                    ClovaMicrophone.onStartSTT();
                    break;
            }
        }
        else
        {
            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_CANCEL);

            switch (state)
            {
                case PressButton.voice:
                    // 포톤 보이스 비활성화 메소드 호출. 
                    VoiceRecorderController.onStopTransmit();
                    break;
                case PressButton.stt:
                    // stt 녹음 비활성화 메소드 호출. 
                    ClovaMicrophone.onStopSTT();
                    break;
            }
        }    
    }

    private void ButtonSetting(bool IsToggle, GameObject ui)
    {
        if (IsToggle)
        {
            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_COMPLETE);
            ui.SetActive(true);
        }
        else
        {
            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_CANCEL);
            ui.SetActive(false);
        }
    }

}
