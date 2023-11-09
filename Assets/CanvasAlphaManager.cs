using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CanvasAlphaManager : MonoBehaviour
{
    //나의 오브젝트의 Canvas Group을 가져온다.
    public CanvasGroup canvasGroup;
    [SerializeField] private float alphachangedTime = 0.5f;
    //이 오브젝트에 달려있는 가져온다.
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //닷 트윈을 사용하여 구릅을 가져온다.

    public void ControlManager(Toggle toggle)
    {
        //toggle을 가져와서 ison 이면 알파값 1
        if (toggle.isOn)
        {
            canvasGroup.DOFade(1, alphachangedTime);
        }
        //isoff이면 알파값 0
        else
        {
            canvasGroup.DOFade(0, alphachangedTime); 
        }

    }

}
