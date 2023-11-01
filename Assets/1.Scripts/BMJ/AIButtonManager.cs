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
    //버튼 프리펩을 가져온다.
    public GameObject AIButton;

    public Transform attachTransform;

    //추가할 부모 Transform을 가져온다.
    public Transform parent;

    // 해당 이벤트 오브젝트 가져오기
    private GameObject EventButtonObj;

    // 해당 이벤트 오브젝트 데이터
    private string EventButtonData;

    // httpManager 싱글톤 인스턴스 
    HttpManager http;

    //5글자 이상인 음식과 관련된 예제 리스트 5개를 만들어보겠다.
    private List<string> data = new List<string>()
    {
        "VR,AR 경험",
        "소셜 네트워킹",
        "경제 시스템",
        "XR 콘텐츠 공유",
        "가상 교육"
    };
    private void Awake()
    {
        http = HttpManager.instance;
    }

    private void Start()
    {
        //Start 대신에 버튼 클릭하실 때 사용하시면 됩니다 !
        CreateAIKeyword();
    }

    private void CreateAIKeyword()
    {
        //// http 통신으로 받은 AI 추천 키워드를 반영한다. 
        //for (int i = 0; i < http.GETAIDATA.Count; i++)
        //{
        //    // AI 키워드 버튼을 생성. 
        //    GameObject button_i = Instantiate(AIButton, parent);

        //    // AI 키워드 버튼의 Text 부분에 데이터를 하나씩 넣는다.
        //    button_i.GetComponentInChildren<TextMeshProUGUI>().text = http.GETAIDATA[i];

        //    //버튼마다 클릭 이벤트를 설정.
        //    Button btn = button_i.GetComponent<Button>();
        //    btn.onClick.AddListener(OnClickButtonEvent);
        //}

        for (int i = 0; i < data.Count; i++)
        {
            GameObject button_i = Instantiate(AIButton, parent);

            //button 프리팹에 있는 버튼의 Text 부분에 데이터를 하나씩 넣는다.
            button_i.GetComponentInChildren<TextMeshProUGUI>().text = data[i];

            //버튼마다 클릭 이벤트를 설정.
            Button btn = button_i.GetComponent<Button>();
            btn.onClick.AddListener(OnClickButtonEvent);
        }
    }

    // 해당 추천 AI 키워드 버튼 클릭 이벤트
    public void OnClickButtonEvent()
    {
        // 해당 이벤트 오브젝트 가져오기
        EventButtonObj = EventSystem.current.currentSelectedGameObject;

        // 해당 이벤트 오브젝트에서 데이터를 가져오기
        EventButtonData = EventButtonObj.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    // 마인드 추가 버튼 클릭 이벤트
    public void AddMind_Btn()
    {
        // 생성하고 싶은 키워드를 선택하고 마인드 추가
        if (EventButtonObj != null)
        {
            // 마인드 해당 오브젝트 생성.
            print("마인드 노드 생성 : " + EventButtonObj.name);
            // 마인드 노드에 데이터 삽입.
            print("마인드 노드 데이터 삽입 : " + EventButtonData);
            PlayerInfo.instance.createBubble = true;
            MindMapController.instance.UpdateCreate(attachTransform, EventButtonData);
            PlayerInfo.instance.createBubble = false;
            // 링크도 연결.

            // AI 키워드 추천 UI창이 사라지고 노드 바로 생성
            //this.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            // 에러 팝업 창 : ( AI KEYWORD를 선택하지 않았습니다. ! )
        }
    }

    // 다시 보기 버튼 클릭 이벤트
    public void Menu_Again_Btn()
    {
        print("다시 보기 버튼 클릭.");
        // AI 서버에서 다시 키워드들을 호출한다.
    }
}