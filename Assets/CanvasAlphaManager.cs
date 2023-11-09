using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CanvasAlphaManager : MonoBehaviour
{
    //���� ������Ʈ�� Canvas Group�� �����´�.
    public CanvasGroup canvasGroup;
    [SerializeField] private float alphachangedTime = 0.5f;
    //�� ������Ʈ�� �޷��ִ� �����´�.
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //�� Ʈ���� ����Ͽ� ������ �����´�.

    public void ControlManager(Toggle toggle)
    {
        //toggle�� �����ͼ� ison �̸� ���İ� 1
        if (toggle.isOn)
        {
            canvasGroup.DOFade(1, alphachangedTime);
        }
        //isoff�̸� ���İ� 0
        else
        {
            canvasGroup.DOFade(0, alphachangedTime); 
        }

    }

}
