using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneManager : MonoBehaviour
{
    private void Start()
    {
        // 시작하면 마이룸 사운드를 실행한다.
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_MYROOM); 
    }

    //다음 씬으로 이동하도록 만든다.
    public void NextScene()
    {
        // 마이룸 사운드 종료.
        SoundManager.instance.StopBGM();
        // 게임 씬으로 전환. 
        SceneManager.LoadScene("IdeaPark_0.4v_Photon");
    }
}