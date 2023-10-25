using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class MindMapController : MonoBehaviour
{
    // 마인드맵 오른쪽 기능 컨트롤러
    public GameObject R_indexTip;

    // 마인드맵 왼쪽 기능 컨트롤러
    public GameObject L_indexTip;

    // 마인드맵 노드들을 부모로 관리하는 오브젝트
    public GameObject nodeManager;

    // 사용자가 생성한 노드의 Info 정보 ( 생성될 때마다 갱신)  
    private MindMapNodeInfo nodeInfo;  

    // R_indexTip 사용 여부
    private bool rightIndexTip = true;

    // 노드의 데이터 정보 업데이트 
    private bool upDateData; 

    // 노드의 연결한 위치를 임시로 저장 
    CreateNodeContoller ConnectionPointNode;

    // 핀치 여부 
    private bool isPinched;

    // 핀치된 오브젝트  
    private GameObject pinch = null;

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
            this.pinch = pinchObject;
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

        
        // 1번 키를 누르면 플레이어가 노드를 생성할 수 있는 CREATE
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            state = State.CREATE;
        }

        // 2번 키를 누르면 플레이어가 노드를 연결할 수 있는 CONNECTION
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            state = State.CONNECTION;
        }

        // 3번 키를 누르면 플레이어가 노드에 데이터를 삭제
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            state = State.DELETE;
        }
    }

    // 마인드 노드를 링크하고, 트리를 연결하는 메소드 
    // 자식 노드로 설정시 유의.
    // 루트 노드의 자식으로 지정. 
    // 로직 -> 자식은 부모보다 id 값이 클수가 없다(예외처리)



    private void UpdateConnection()
    {
       
        if (hover != null)
        {
            print("현재 손가락에 닿은 오브젝트 : " + hover.GetComponentInChildren<MindMapNodeInfo>().ID);

            CreateNodeContoller updateConnectionNode  = hover.GetComponentInChildren<CreateNodeContoller>();


            // X번 키를 누르면 활성화된 "링크"를 허공에 끌어 당기면 자동으로 R_indexTip의 위치에 라인렌더러가 생긴다. (QA 완료)
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (rightIndexTip)
                {
                    updateConnectionNode.ConnectionIndexNode(R_indexTip, true);
                    ConnectionPointNode = updateConnectionNode;
                    rightIndexTip = false;
                }
               
            }

            // C번 키를 누르면 라인렌더러를 연결, 마인드맵의 부모 노드와 자식 노드를 연결. 
            // 1. "링크"를 허공에 끌어 당기고, 연결할 노드를 찾으면 노드가 연결된다.
            // 2.  "링크"를 허공에 끌어 당기고, 연결할 노드를 찾지 못하면 "링크" 가 사라진다. 
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!rightIndexTip)
                {
                    //// 사용자로 부터 입력받아 연결할 노드 정보
                    //MindMapNodeInfo selectedInfo = hover.GetComponentInChildren<MindMapNodeInfo>();

                    //// 생성한 노드 정보 : nodeInfo, 연결할 노드 정보 : hover.GetComponentInChildren<MindMapNodeInfo>() 를 ID 값을 비교한다. 
                    //bool nodeCheck = (nodeInfo.ID > selectedInfo.ID) ? true : false;

                    //print("ID 값 확인 : " + "NODEINFO.ID : " + nodeInfo.ID + "selectedInfo.ID : " + selectedInfo.ID);


                    //// 생성한 노드 ID 가 높은 경우 -> CreateNode 생성한 노드(부모),  target를 연결할 노드(자식) 
                    //if (nodeCheck)
                    //{

                    //    // CreateNode(부모) 자식 노드 리스트 Children에 target (자식)을 추가한다. 
                    //    nodeInfo.Children.Add(selectedInfo);

                    //}
                    //// 연결한 노드 ID가 높은 경우 -> 연결한 노드(부모) , 생성한 노드(자식)
                    //else
                    //{
                    //    // target(부모) 자식 노드 리스트 Children에 CreateNode (자식)을 추가한다.  
                    //    selectedInfo.Children.Add(nodeInfo);
                    //}

                    // 마인드맵에 대한 링크를 연결. 
                    ConnectionPointNode.ConnectionIndexNode(updateConnectionNode.transform.gameObject, false);

                    //INDEXDISTAL과 연결된 라인렌더러 노드를 찾아서 삭제. (QA 완료)
                    ConnectionPointNode.OnDestroyindexObject();

                    rightIndexTip = true;
                }

            }

            
        }
 
    }

    // 사용자가 마인드 노드에 텍스트를 입력하는 메소드
    private void UpdateInputText()
    {
        print("텍스트 입력창 실행 : 노드 데이터 출력 : " + hover.GetComponentInChildren<MindMapNodeInfo>().DATA);

        // 현재 입력하려는 노드 
        MindMapNodeInfo nodeInfo = hover.GetComponentInChildren<MindMapNodeInfo>();

        
       
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
     
        // 키보드 V번 키를 누르면 선택된 마인드 노드를 확인하고 삭제.
        
        if(Input.GetKeyDown(KeyCode.V)) {

            MindMapNodeInfo deleteNode = hover.GetComponentInChildren<MindMapNodeInfo>();
            CreateNodeContoller deleteNodeController = hover.GetComponentInChildren<CreateNodeContoller>();

            // 1. 중간 노드를 삭제하는 경우는 서브트리 전체를 삭제 한다.

            // 2. 리프 노드를 삭제하는 경우는 해당 리프 노드를 찾아서 삭제한다. 
            if (deleteNode.Children.Count == 0)
            {
                print("노드 삭제");

                // 1. 연결되어 있는 노드 렌더러들을 모두 삭제.
               

                // 2. 해당 삭제할 노드 삭제 
                Destroy(deleteNode.transform.parent.gameObject);
            }
        }




    }

    // 마인드 노드 선택하는 메소드 
    private void UpdateSeleted()
    {
        // 마인드 선택 
        // 검지의 TIP을 구체 POKE를 하면 마인드 선택 상태가 되면 마인드 UI가 실행된다.

        // 서브
    }

    // 마인드 노드를 생성하는 메소드 
    private void UpdateCreate()
    {

        // z번 키를 누르면 사용자 R_indexTip의 위치에서 노드가 생성된다. (QA 완료 )
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/Test_Node");
            GameObject CreateNode = Instantiate(obj, R_indexTip.transform.position, Quaternion.identity);

            // 생성된 노드들은 [ MindMapManager ]의 하위에 저장된다,
            //CreateNode.transform.SetParent(NodeManager.transform);

            nodeInfo = CreateNode.GetComponentInChildren<MindMapNodeInfo>();

            // 노드의 ID 값을 증가. 
            nodeInfo.UpdateNodeId();

            if (nodeInfo.ID == 0)
            {
                // 루트 노드(주제)로 지정한다. 
                nodeInfo.ROOTNODE = true;

                // MindeMapManager의 트리 루트로 설정.
                MindMapManager.instance.ROOTNODE = nodeInfo; 
            }
           
        }

        


    }
}
