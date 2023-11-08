using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneManager : MonoBehaviour
{
    //다음 씬으로 이동하도록 만든다.
    public void NextScene()
    {
        SceneManager.LoadScene("IdeaPark_0.3v");
    }
}