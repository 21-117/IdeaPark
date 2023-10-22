using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MindMapController : MonoBehaviour
{
    // 마인드맵 CRUD  기능 컨트롤러

    [HideInInspector]
    public Transform R_indexTip;

    // 핀치 여부 
    private bool isPinched;

    // 핀치된 오브젝트  
    private GameObject target = null;

    // Hover 여부
    private bool isHovered;

    // Hover된 오브젝트 
    private GameObject hover = null;

    // 핀치 변수, 오브젝트 설정
    public static Action<bool, GameObject> setPinch;

    // Hover된 오브젝트 정보 
    public static Action<bool, GameObject> setHover;

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

    private void UpdateConnection()
    {

        //  마인드 연결(링크)
        // "선택하지 않은 마인드"를 PICH하면 링크가 활성화 된다.

       
        // 자식 노드로 설정시 유의.
        // 루트 노드의 자식으로 지정. 
        // 로직 -> 자식은 부모보다 id 값이 클수가 없다(예외처리)

       
        if (isPinched && isHovered)
        { 
            //활성화된 "링크"를 허공에 끌어 당기면 자동으로 마인드가 생성되면, 자동으로 텍스트 입력 상태가된다.
            CreataNodeConnection.connection(R_indexTip, true);

          

        }


       
        //if (hover != target)
        //{
           


        //    if (hover == target && isPinched)
        //    {
        //        print("왜 여기는 실행이 안되냐 ");
        //        CreataNodeConnection.destroyConnection();
        //    }
          
        //}

        // Target 오브젝트에 노드를 연결한 다음
        //CreataNodeConnection.connection(targetPos, false);






        //state = State.IDLE;
    }

    private void UpdateInputText()
    {
        print("텍스트 입력창 실행,"); 
    }

    private void UpdateAiText()
    {
        throw new NotImplementedException();
    }

    private void UpdateColor()
    {
        throw new NotImplementedException();
    }

    private void UpdateDelete()
    {
        // 마인드 삭제 
        // "마인드 선택 상태에서 실행된 "마인드 UI "중 DELETE를 선택해 해당 마인드를 삭제
        // 키보드 2번 키를 누르면 선택된 마인드 노드를 확인하고 삭제. 
    }

    private void UpdateSeleted()
    {
        // 마인드 선택 
        // 검지의 TIP을 구체 POKE를 하면 마인드 선택 상태가 되면 마인드 UI가 실행된다.
    }

    private void UpdateCreate()
    {
       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            GameObject obj = Resources.Load<GameObject>("Prefabs/Test_Node");
            GameObject node = Instantiate(obj, R_indexTip.position, Quaternion.identity);

            if(node.GetComponentInChildren<MindMapNodeInfo>().ID == 1)
            {
                // 루트 노드(주제)로 지정한다. 
                node.GetComponentInChildren<MindMapNodeInfo>().ROOTNODE = true; 
            }

            state = State.CONNECTION;
        }


    }
}
