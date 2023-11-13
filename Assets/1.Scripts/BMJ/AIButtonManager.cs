using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static System.Net.WebRequestMethods;

public class AIButtonManager : MonoBehaviour
{
    //��ư �������� �����´�.
    public GameObject AIButton;

    public Transform attachTransform;

    //�߰��� �θ� Transform�� �����´�.
    public Transform parent;

    // AI �ؽ�Ʈ ���ε� ��� �߰� ��ư
    public GameObject addMindNodeBtn;

    // AI �ؽ�Ʈ ���� ��ư 
    public GameObject updateMindNodeBtn;

    // �ش� �̺�Ʈ ������Ʈ ��������
    private GameObject EventButtonObj;

    // �ش� �̺�Ʈ ������Ʈ ������
    private string EventButtonData;

    // httpManager �̱��� �ν��Ͻ� 
    HttpManager http;

    // AI �޴� ��ư ���
    private Toggle toggle;

    //5���� �̻��� ���İ� ���õ� ���� ����Ʈ 5���� �����ڴ�.
    private List<string> data = new List<string>()
    {
        "VR,AR ����",
        "�Ҽ� ��Ʈ��ŷ",
        "���� �ý���",
        "XR ������ ����",
        "���� ����"
    };
    private void Awake()
    {
        // AI �޴� ��ư 
        GameObject aiBtn = GameObject.Find("Button_AI"); 
        toggle = aiBtn.GetComponent<Toggle>();
        http = HttpManager.instance;
    }

    private void Start()
    {
        //Start ��ſ� ��ư Ŭ���Ͻ� �� ����Ͻø� �˴ϴ� !
        CreateAIKeyword();

        // AI �ؽ�Ʈ ���ε� ��� �߰� ��ư ������
        addMindNodeBtn.GetComponent<Button>().onClick.AddListener(AddMind_Btn);

        // AI �ؽ�Ʈ ���� ��ư ������ 
        updateMindNodeBtn.GetComponent<Button>().onClick.AddListener(Menu_Again_Btn);
    }

    private void CreateAIKeyword()
    {
        //// http ������� ���� AI ��õ Ű���带 �ݿ��Ѵ�. 
        //for (int i = 0; i < http.GETAIDATA.Count; i++)
        //{
        //    // AI Ű���� ��ư�� ����. 
        //    GameObject button_i = Instantiate(AIButton, parent);

        //    // AI Ű���� ��ư�� Text �κп� �����͸� �ϳ��� �ִ´�.
        //    button_i.GetComponentInChildren<TextMeshProUGUI>().text = http.GETAIDATA[i];

        //    //��ư���� Ŭ�� �̺�Ʈ�� ����.
        //    Button btn = button_i.GetComponent<Button>();
        //    btn.onClick.AddListener(OnClickButtonEvent);
        //}

        for (int i = 0; i < data.Count; i++)
        {
            GameObject button_i = Instantiate(AIButton, parent);

            //button �����տ� �ִ� ��ư�� Text �κп� �����͸� �ϳ��� �ִ´�.
            button_i.GetComponentInChildren<TextMeshProUGUI>().text = data[i];

            //��ư���� Ŭ�� �̺�Ʈ�� ����.
            Button btn = button_i.GetComponent<Button>();
            btn.onClick.AddListener(OnClickButtonEvent);
        }
    }

    // �ش� ��õ AI Ű���� ��ư Ŭ�� �̺�Ʈ
    public void OnClickButtonEvent()
    {
        // AI Ű���� Ŭ�� ����
        SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UIMENU_ONCLICK);

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
            // ���ε� �߰� ��ư ���� ����
            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_CANCEL);

            // ���ε� ��� ���� ���
            PlayerInfo.localPlayer.createBubble = true;

            // ��ũ�� ����.

            // AI Ű���� ���ε� ��� ����. 
            MindMapController.CreateMindNode(attachTransform.position, EventButtonData);

            // ���ε� ��� ���� �Ұ� 
            PlayerInfo.localPlayer.createBubble = false;

            // AI Ű��Ʈ �޴� ��� ��Ȱ��ȭ
            toggle.isOn = false;

            // AI Ű���� ��õ UIâ�� �������.
            this.transform.parent.gameObject.SetActive(false);

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

        // �ٽ� ���� ��ư Ŭ�� ���� ����
        SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_COMPLETE);
    }
}