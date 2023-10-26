using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Scene�� BGM ���� [ �ε�, ���̷�, ������Ʈ(��ũ�����̽�), ���� ]
    public enum EBgm
    {
        BGM_LODING,
        BGM_MYROOM,
        BGM_WORKSPACE,
        BGM_TEAMSPACE
    }

    // SFX ����
    // 1. ������Ʈ ���� SFX 
    // 2. ���ε� �� SFX [ ���ε� ����, ���ε� ����, ���ε� Ŭ��]
    // 3. ���ε� UI SFX [ ���ε� UI ����, ���ε� UI Ŭ��, UI�޴� Ŭ��, UI Ȯ��, UI ��� ]
    // 4. Ű���� SFX [ Ű���� Ÿ����, Ű���� ����� ] 
    public enum ESFX
    {
        SFX_OPENING,
        SFX_NODE_CREATE,
        SFX_NODE_DELETE,
        SFX_NODE_SELECT,
        SFX_UI_CREATE,
        SFX_UI_SELECT,
        SFX_UIMENU_SELECT,
        SFX_UI_COMPLETE,
        SFX_UI_CANCEL
    }

    // Scene�� BGM Audio Clip�� ���� �� �ִ� �迭
    public AudioClip[] bgms;

    // SFX Audop Clip�� ���� �� �ִ� �迭
    public AudioClip[] sfxs;

    // BGM�� �÷����ϴ� AudioSource
    public AudioSource audioBGM;

    // SFX�� �÷����ϴ� AudioSource
    public AudioSource audioSFX;

    // �̱��� ���� 
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
        // �÷����� bgm Audio Clip�� ����
        audioBGM.clip = bgms[(int) bgmIdx]; 
        // BGM �÷��� 
        audioBGM.Play();
    }
    
    // BGM STOP 
    public void StopBGM()
    {
        audioBGM.Stop(); 
    }

    // SFX PLAY 
    public void PlaySFX(ESFX sfxIdx)
    {
        // SFX �÷��� 
        audioSFX.PlayOneShot(sfxs[(int) sfxIdx]);   
    }






}
