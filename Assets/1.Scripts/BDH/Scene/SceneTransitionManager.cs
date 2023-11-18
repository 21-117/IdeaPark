using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeInOutScreen fadeScreen;

    private void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_MYROOM);
    }

    // �� �̵� �޼ҵ�
    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeOutDuration);

        // �� ��ȯ.
        SceneManager.LoadScene(sceneIndex);
    }

    // �񵿱������� �� ��ȯ �޼ҵ�. 
    public void GoToSceneAsync(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeScreen.fadeOutDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}

