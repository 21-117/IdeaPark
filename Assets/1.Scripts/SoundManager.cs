using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Scene�� BGM ���� [ ���̷�, ������Ʈ(��ũ�����̽�), �ε� ]
    public enum EBgm
    {
        BGM_MYROOM, // Myroom.wav
        BGM_WORKSPACE, // space.wav
        BGM_LODING // ����
    }

    // SFX ����
    // 1. ������Ʈ SFX 
    // 2. ���ε� �� SFX [ ���ε� ����, ���ε� ����, ���ε� Ŭ��]
    // 3. ���ε� UI SFX [ ���ε� UI �޴� ���� , ���ε� UI Ŭ�� Ȯ��, UI�޴� Ŭ��, UI Ȯ��, UI ��� ]
    // 4. Ű���� SFX [ Ű���� Ÿ����, Ű���� ����� ] 
    public enum ESFX
    {
        SFX_OPENING, // opening
        SFX_NODE_CREATE, // mind_create
        SFX_NODE_DELETE, // mind_delete
        SFX_NODE_ONCLICK, // mind_click
        SFX_UI_CREATE, // mind_ui_open
        SFX_UI_COMPLETE, // ui_complete
        SFX_UIMENU_ONCLICK, // ui_click
        SFX_UI_CANCEL, // ui_cancle
        SFX_KEYBOARD_ONCLICK, // keyboard
        SFX_KEYBOARD_DELETE, // keyboard_backspace
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
        if (instance == null)
        {
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
        audioBGM.clip = bgms[(int)bgmIdx];
        // BGM �÷��� 
        audioBGM.Play();

        // BGM �÷��� �ݺ�.
        audioBGM.loop = true;
    }

    // BGM STOP 
    public void StopBGM()
    {
        audioBGM.Stop();
    }

    // ������Ʈ SFX PLAY 
    public void PlaySFX(ESFX sfxIdx)
    {
        // SFX �÷��� 
        audioSFX.PlayOneShot(sfxs[(int)sfxIdx]);
    }

}
