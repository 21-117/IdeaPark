using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MindMapController : MonoBehaviour
{
    // 마인드맵 CRUD  기능 컨트롤러

    [HideInInspector]
    public Transform R_indexTip;

    // R_indexTip 연결 여부
    private bool indexTip = true;

    // 핀치 여부 
    private bool isPinched;

    // 핀치된 오브젝트  
    private GameObject target = null;

    // 핀치된 target 오브젝트 CreataNodeConnection 스크립트
    CreataNodeConnection tempNodeCon;

    // Hover 여부
    private bool isHovered;

    // Hover된 오브젝트 
    private GameObject hover = null;

    // Poke 여부
    private bool isPoked;

    // Poke된 오브젝트
    private GameObject poke = null;

    // 핀치된 셋팅 정보
    public static Action<bool, GameObject> setPinch;

    // Hover된 셋팅 정보 
    public static Action<bool, GameObject> setHover;

    // POKE된 셋팅 정보
    public static Action<bool, GameObject> setPoke; 

    // Poke에 대한 프로퍼티
    public bool ISPOKED
    {
        get { return isPoked; }
        set { isPoked = value;  }
    }

    // 핀치에 대한 프로퍼티
    public bool ISPINCHED
    {
        get { return isPinched; }
        set { isPinched = value; }
    }

    // Hover에 대한 프로퍼티
    public bool ISHOVERD
    {
        get { return isHovered; }
        set { isHovered = value; }
    }

    public enum State
    {
        IDLE,
        CREATE,
        SELETED,
        DELETE,
        UPDATECOLOR,
        AITEXT,
        INPUTTEXT,
        CONNECTION,
    }

    public static State state;

    private void Awake()
    {
        // MindMap를 관리하는 싱글톤 클래스를 생성한다. 
        MindMap.Get();
    }

    void Start()
    {

        setHover = (x, hoverObject) =>
        {
            ISHOVERD = x;
            this.hover = hoverObject;
        };

        setPinch = (x, pinchObject) =>
        {
            ISPINCHED = x;
            this.target = pinchObject;
        };

        setPoke = (x, pokeObject) =>
        {
            ISPOKED = x;
            this.poke = pokeObject;
        };


        state = State.CREATE;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.IDLE:
                break;
            case State.CREATE:
                UpdateCreate();
                break;
            case State.SELETED:
                UpdateSeleted();
                break;
            case State.DELETE:
                UpdateDelete();
                break;
            case State.UPDATECOLOR:
                UpdateColor();
                break;
            case State.AITEXT:
                UpdateAiText();
                break;
            case State.INPUTTEXT:
                UpdateInputText();
                break;
            case State.CONNECTION:
                UpdateConnection();
                break;
            default:
                break;
        }
    }

    // 마인드 노드를 링크하고, 트리를 연결하는 메소드 
    // 자식 노드로 설정시 유의.
    // 루트 노드의 자식으로 지정. 
    // 로직 -> 자식은 부모보다 id 값이 클수가 없다(예외처리)
    private void UpdateConnection()
    {
        //  마인드 연결(링크)
        // "선택하지 않은 마인드"를 PICH하면 링크가 활성화 된다.
        if (isPinched && isHovered)
        {
            if (indexTip)
            {
                tempNodeCon = target.GetComponentInChildren<CreataNodeConnection>();
                //활성화된 "링크"를 허공에 끌어 당기면 자동으로 마인드가 생성되면, 자동으로 텍스트 입력 상태가된다.
                tempNodeCon.ConnectionIndexNode(R_indexTip, true);

                indexTip = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tempNodeCon.ConnectionIndexNode(target.transform, false);

        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            tempNodeCon.OnDestroyindexObject();
            indexTip = true;
        }

        isPinched = false;
        
    }

    // 사용자가 마인드 노드에 텍스트를 입력하는 메소드
    private void UpdateInputText()
    {
        print("텍스트 입력창 실행,");
    }

    // 서버로 부터 AIText 정보를 받아 처리하는 메소드 
    private void UpdateAiText()
    {
        throw new NotImplementedException();
    }

    // 마인드 노드의 색상을 변경하는 메소드. 
    private void UpdateColor()
    {
        throw new NotImplementedException();
    }

    // 마인드 노드를 삭제하는 메소드 
    private void UpdateDelete()
    {
        // 마인드 삭제 
        // "마인드 선택 상태에서 실행된 "마인드 UI "중 DELETE를 선택해 해당 마인드를 삭제
        // 키보드 2번 키를 누르면 선택된 마인드 노드를 확인하고 삭제. 
    }

    // 마인드 노드 선택하는 메소드 
    private void UpdateSeleted()
    {
        // 마인드 선택 
        // 검지의 TIP을 구체 POKE를 하면 마인드 선택 상태가 되면 마인드 UI가 실행된다.
    }

    // 마인드 노드를 생성하는 메소드 
    private void UpdateCreate()
    {

            GameObject obj = Resources.Load<GameObject>("Bubble");
            GameObject node = Instantiate(obj, R_indexTip.position, Quaternion.identity);

            if (node.GetComponentInChildren<MindMapNodeInfo>().ID == 1)
            {
                // 루트 노드(주제)로 지정한다. 
                node.GetComponentInChildren<MindMapNodeInfo>().ROOTNODE = true;
            }

            //state = State.CONNECTION;


    }
}
