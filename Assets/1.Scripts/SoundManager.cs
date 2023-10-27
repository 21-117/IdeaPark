using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Scene별 BGM 종류 [ 로딩, 마이룸, 프로젝트(워크스페이스), 협업 ]
    public enum EBgm
    {
        BGM_LODING,
        BGM_MYROOM,
        BGM_WORKSPACE,
        BGM_TEAMSPACE
    }

    // SFX 종류
    // 1. 프로젝트 SFX 
    // 2. 마인드 맵 SFX [ 마인드 생성, 마인드 삭제, 마인드 클릭]
    // 3. 마인드 UI SFX [ 마인드 UI 메뉴 생성 , 마인드 UI 클릭 확인, UI메뉴 클릭, UI 확인, UI 취소 ]
    // 4. 키보드 SFX [ 키보드 타건음, 키보드 지우기 ] 
    public enum ESFX
    {
        SFX_OPENING,
        SFX_NODE_CREATE,
        SFX_NODE_DELETE,
        SFX_NODE_ONCLICK,
        SFX_UI_CREATE,
        SFX_UI_ONCLICK,
        SFX_UIMENU_ONCLICK,
        SFX_UI_COMPLETE,
        SFX_UI_CANCEL,
        SFX_KEYBOARD_ONCLICK,
        SFX_KEYBOARD_DELETE,
    }

    // Scene별 BGM Audio Clip을 담을 수 있는 배열
    public AudioClip[] bgms;

    // SFX Audop Clip을 담을 수 있는 배열
    public AudioClip[] sfxs;

    // BGM을 플레이하는 AudioSource
    public AudioSource audioBGM;

    // SFX을 플레이하는 AudioSource
    public AudioSource audioSFX;

    // 싱글톤 패턴 
    public static SoundManager instance;


    private void Awake()
    {
        if(instance == null) { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM PLAY 
    public void PlayBGM(EBgm bgmIdx)
    {
        // 플레이할 bgm Audio Clip을 설정
        audioBGM.clip = bgms[(int) bgmIdx]; 
        // BGM 플레이 
        audioBGM.Play();
    }
    
    // BGM STOP 
    public void StopBGM()
    {
        audioBGM.Stop(); 
    }

    // 프로젝트 SFX PLAY 
    public void PlaySFX(ESFX sfxIdx)
    {
        // SFX 플레이 
        audioSFX.PlayOneShot(sfxs[(int) sfxIdx]);   
    }

}
