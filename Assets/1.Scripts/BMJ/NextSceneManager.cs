using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneManager : MonoBehaviour
{
    //���� ������ �̵��ϵ��� �����.
    public void NextScene()
    {
        SceneManager.LoadScene("IdeaPark_0.3v");
    }
}