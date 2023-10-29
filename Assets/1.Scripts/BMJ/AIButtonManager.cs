using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AIButtonManager : MonoBehaviour
{
    //��ư �������� �����´�.
    public GameObject AIButton;
    public Transform attachTransform;

    //�߰��� �θ� Transform�� �����´�.
    public Transform parent;

    // �ش� �̺�Ʈ ������Ʈ �������� 
    private GameObject EventButtonObj;

    // �ش� �̺�Ʈ ������Ʈ ������ 
    private string EventButtonData;

    //5���� �̻��� ���İ� ���õ� ���� ����Ʈ 5���� �����ڴ�.
    private List<string> data = new List<string>()
    {
        "���� �ȳ�",
        "ġŲ ����",
        "���� ��Ʈ",
        "�ܹ��� ���ִ�",
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
            GameObject button_i = Instantiate(AIButton, parent);

            //button �����տ� �ִ� ��ư�� Text �κп� �����͸� �ϳ��� �ִ´�.
            button_i.GetComponentInChildren<TextMeshProUGUI>().text = data[i];

            // ��ư���� Ŭ�� �̺�Ʈ�� ����.
            Button btn = button_i.GetComponent<Button>();
            btn.onClick.AddListener(OnClickButtonEvent);

        }
    }

    // �ش� ��õ AI Ű���� ��ư Ŭ�� �̺�Ʈ 
    public void OnClickButtonEvent()
    {

        // �ش� �̺�Ʈ ������Ʈ �������� 
        EventButtonObj = EventSystem.current.currentSelectedGameObject;

        // �ش� �̺�Ʈ ������Ʈ���� �����͸� �������� 
        EventButtonData = EventButtonObj.GetComponentInChildren<TextMeshProUGUI>().text;




    }

    // ���ε� �߰� ��ư Ŭ�� �̺�Ʈ 
    public void AddMind_Btn()
    {
        // �����ϰ� ���� Ű���带 �����ϰ� ���ε� �߰� 
        if (EventButtonObj != null)
        {
            // ���ε� �ش� ������Ʈ ����.
            print("���ε� ��� ���� : " + EventButtonObj.name);
            // ���ε� ��忡 ������ ����. 
            print("���ε� ��� ������ ���� : " + EventButtonData);
            PlayerInfo.instance.createBubble = true;
            MindMapController.instance.UpdateCreate(attachTransform, EventButtonData);
            PlayerInfo.instance.createBubble = false;
            // ��ũ�� ����. 

            // AI Ű���� ��õ UIâ�� ������� ��� �ٷ� ����
            //this.transform.parent.gameObject.SetActive(false);

        }
        else
        {
            // ���� �˾� â : ( AI KEYWORD�� �������� �ʾҽ��ϴ�. ! )
        }

    }

    // �ٽ� ���� ��ư Ŭ�� �̺�Ʈ 
    public void Menu_Again_Btn()
    {
        print("�ٽ� ���� ��ư Ŭ��.");
        // AI �������� �ٽ� Ű������� ȣ���Ѵ�. 
    }

}