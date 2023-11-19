//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.Experimental.XR.Interaction;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using static System.Net.WebRequestMethods;

//public enum PressButton
//{
//    none,
//    ai,
//    palette,
//    keyboard,
//    delete,
//    voice,
//    stt,
//    home
//}
//public class PressMindButton : MonoBehaviourPunCallbacks
//{
//    public PressButton state;
//    [HideInInspector]
//    public GameObject ai;
//    [HideInInspector]
//    public GameObject palette;
//    [HideInInspector]
//    public GameObject keyboard;
//    [HideInInspector]
//    public GameObject delete;

//    private Toggle toggle;

//    private void Awake()
//    {
//        toggle = GetComponent<Toggle>();    
//    }

//    private void Update()
//    {
//        if (PlayerInfo.localPlayer != null)
//        {
//            ai = PlayerInfo.localPlayer.ai;
//            palette = PlayerInfo.localPlayer.palette;
//            keyboard = PlayerInfo.localPlayer.keyboard;
//            delete = PlayerInfo.localPlayer.delete;
//        }
//    }

   

//    public void OnOffButton()
//    {
//        switch(state)
//        {
//            case PressButton.ai:             
//                ButtonSetting(toggle.isOn, ai); 
//                break;
//            case PressButton.palette:
//                ButtonSetting(toggle.isOn, palette);
//                break;
//            case PressButton.keyboard:
//                ButtonSetting(toggle.isOn, keyboard);
//                break;
//            case PressButton.delete:
//                CallbackMethod(toggle.isOn, PressButton.delete);
//                break;
//            case PressButton.voice:
//                CallbackMethod(toggle.isOn, PressButton.voice);
//                break;
//            case PressButton.stt:
//                CallbackMethod(toggle.isOn, PressButton.stt);
//                break; 
//            case PressButton.home:
//                CallbackMethod(toggle.isOn, PressButton.home);
//                break; 

//        }

//    }

//    private void CallbackMethod(bool IsToggle, PressButton state)
//    {
//        if (IsToggle)
//        {
//            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_COMPLETE);

//            switch (state)
//            {
//                case PressButton.delete:
//                    // ������ �޼ҵ� ȣ��.
//                    //MindMapController.mindMapController.UpdateDelete();
//                    toggle.isOn = false;
//                    break;
//                case PressButton.voice:
//                    // ���� ���̽� Ȱ��ȭ �޼ҵ� ȣ��.
//                    VoiceRecorderController.onStartTransmit();
//                    break;
//                case PressButton.stt:
//                    // stt ���� Ȱ��ȭ �޼ҵ� ȣ��. 
//                    ClovaMicrophone.onStartSTT();
//                    break;
//                case PressButton.home:
//                    // home 
//                    SceneManager.LoadScene("MJB_MyRoomScene2");
//                    break;
//            }
//        }
//        else
//        {
//            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_CANCEL);

//            switch (state)
//            {
//                case PressButton.voice:
//                    // ���� ���̽� ��Ȱ��ȭ �޼ҵ� ȣ��. 
//                    VoiceRecorderController.onStopTransmit();
//                    break;
//                case PressButton.stt:
//                    // stt ���� ��Ȱ��ȭ �޼ҵ� ȣ��. 
//                    ClovaMicrophone.onStopSTT();
//                    break;
//            }
//        }    
//    }

//    private void ButtonSetting(bool IsToggle, GameObject ui)
//    {
//        if (IsToggle)
//        {
//            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_COMPLETE);
//            ui.SetActive(true);
//        }
//        else
//        {
//            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_CANCEL);
//            ui.SetActive(false);
//        }
//    }

//}
