using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneManager : MonoBehaviour
{
    private void Start()
    {
        // �����ϸ� ���̷� ���带 �����Ѵ�.
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_MYROOM); 
    }

    //���� ������ �̵��ϵ��� �����.
    public void NextScene()
    {
        // ���̷� ���� ����.
        SoundManager.instance.StopBGM();
        // ���� ������ ��ȯ. 
        SceneManager.LoadScene("IdeaPark_0.4v_Photon");
    }
}