using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIButtonManager : MonoBehaviour
{
    //��ư �������� �����´�.
    public GameObject AIButton;

    //�߰��� �θ� Transform�� �����´�.
    public Transform parent;

    //5���� �̻��� ���İ� ���õ� ���� ����Ʈ 5���� �����ڴ�.
    private List<string> data = new List<string>()
    {
        "���� �ȳ�",
        "ġŲ ����",
        "�ܹ��� ���ִ�",
        "���� ��Ʈ",
        "���� ��Ʈ",
        "���� ��Ʈ",
        "���� ��Ʈ",
        "���� ��Ʈ",
        "���� ��Ʈ",
        "���ҸӴ� ����"
    };

    private void Start()
    {
        //Start ��ſ� ��ư Ŭ���Ͻ� �� ����Ͻø� �˴ϴ� !
        CreateAIKeyword();
    }

    private void CreateAIKeyword()
    {
        //�θ� �����ؼ� Prefab�� �����Ѵ�.
        for (int i = 0; i < data.Count; i++)
        {
            GameObject button = Instantiate(AIButton, parent);
            //button �����տ� �ִ� ��ư�� Text �κп� �ϳ��� �ִ´�.
            button.GetComponentInChildren<TextMeshProUGUI>().text = data[i];
        }
    }
}