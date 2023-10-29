using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class MindMapController : MonoBehaviour
{
    public static MindMapController instance;

    // 마인드맵 오른쪽 기능 컨트롤러
    public GameObject R_indexTip;

    // 마인드맵 왼쪽 기능 컨트롤러
    public GameObject L_indexTip;

    // 마인드맵 노드들을 부모로 관리하는 오브젝트
    private GameObject mindNodeManager;

    // Hover된 오브젝트 
    private GameObject hover = null;

    // 사용자가 생성한 노드의 Info 정보  
    private MindMapNodeInfo nodeInfo;

    // 갱신된 현재 노드의 라인렌더러 controller 
    public ConnectionNodeController currentConnectionNode = null;

    // 이전 노드의 임시 라인렌더러 controller 
    private ConnectionNodeController prevConnectionNode;

    // R_indexTip 사용 여부
    private bool rightIndexTip = true;

    // 노드의 데이터 정보 업데이트 
    private bool upDateData;

    // 핀치 여부 
    private bool isPinched;

    // 핀치된 오브젝트  
    private GameObject pinch = null;

    // Hover 여부
    private bool isHovered;

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
        set { isPoked = value; }
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

    public enum Bubble
    {
        first,
        second,
        third
    }

    public static State state;
    public static Bubble bubble;

    private void Awake()
    {
        mindNodeManager = GameObject.Find("[ MINDNODE MANAGER ]");


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        bubble = Bubble.first;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {

            case State.CREATE:
                UpdateCreate(R_indexTip.transform, "");
                break;
            case State.SELETED:
                UpdateSeleted();
                break;
            case State.DELETE:
                //UpdateDelete();
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

    public void firstNode()
    {
        if (rightIndexTip)
        {
            currentConnectionNode.ConnectionNode(R_indexTip, true);
            prevConnectionNode = currentConnectionNode;
            rightIndexTip = false;
        }
    }

    public void lastNode()
    {
        //라인렌더러를 연결, 마인드맵의 부모 노드와 자식 노드를 연결. 
        if (!rightIndexTip)
        {
            MindMapNodeInfo prevNodeInfo = prevConnectionNode.GetComponent<MindMapNodeInfo>();
            MindMapNodeInfo currentNodeInfo = currentConnectionNode.GetComponent<MindMapNodeInfo>();

            //// 이전에 임시 저장 노드 정보, 현재 연결할 노드 정보 : ID 값을 비교한다. 
            bool nodeIdCheck = (prevNodeInfo.ID < currentNodeInfo.ID) ? true : false;



            // 1.예외 처리 - > 만약에 현재 연결할 노드 (자식)가 다른 부모를 가진 경우( 리프 노드가 아닌 경우 ) -> 연결을 허용해야 하는가..? 
            // 허용한 다면 체크하는 방법은 -> 연결할 노드 (자식)의 라인렌더러 오브젝트를 가지고 있는 지 확인하면 된다. 
            // 연결할 노드 (자식)의 라인렌더러 오브젝트가 존재한다면 -> 리프 노드가 아님. 

            // 2. 예외 처리 - > FindGetHeight(MindMapNodeInfo root) 함수를 통해 마인드 맵의 깊이를 확인
            // 깊이가 같은 노드들끼리는 연결할 수 없도록 설정. 



            if (nodeIdCheck)
            {
                // 이전에 생성한 노드 ID 가 낮은 경우 -> 이전에 생성한 노드(부모),  현재 연결할 노드 노드(자식) 
                // 이 경우에만 부모 - 자식 관계가 형성될 수 있다.

                print("ID 값 확인 : " + "이전에 생성한 노드 정보 : " + prevNodeInfo.ID + "  " + "현재 연결할 노드 정보 : " + currentNodeInfo.ID);

                // "링크"를 허공에 끌어 당기고, 연결할 노드를 찾으면 노드가 연결된다. ( 부모 - 자식) 
                prevConnectionNode.ConnectionNode(currentConnectionNode.transform.gameObject, false);

                // prevConnectionNode(부모) 자식 노드 리스트 Children에 currentConnectionNode (자식) 노드를 추가한다. 
                prevNodeInfo.Children.Add(currentNodeInfo);

            }
            else
            {
                // 이전에 생성한 노드 ID 가 높은 경우 단순히 연결만 지원한다. (양 방향 링크 지원)
                // 제한 사항 -> 자식에서 부모로 연결할 수 없다. 
                currentConnectionNode.ConnectionNode(prevConnectionNode.transform.gameObject, false);

            }

            //  "링크"를 허공에 끌어 당기고, 연결할 노드를 찾지 못하면 "링크" 가 사라진다. 
            prevConnectionNode.OnDestroyIndexTip(R_indexTip);

            rightIndexTip = true;
        }
    }

    // 마인드 노드를 링크하고, 트리를 연결하는 메소드 
    public void UpdateConnection()
    {
        if (PlayerInfo.instance.rayObject != null)
        {
            currentConnectionNode = PlayerInfo.instance.rayObject.GetComponentInChildren<ConnectionNodeController>();

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (rightIndexTip)
                {
                    currentConnectionNode.ConnectionNode(R_indexTip, true);
                    prevConnectionNode = currentConnectionNode;
                    rightIndexTip = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!rightIndexTip)
                {
                    MindMapNodeInfo prevNodeInfo = prevConnectionNode.GetComponent<MindMapNodeInfo>();
                    MindMapNodeInfo currentNodeInfo = currentConnectionNode.GetComponent<MindMapNodeInfo>();

                    //// 이전에 임시 저장 노드 정보, 현재 연결할 노드 정보 : ID 값을 비교한다. 
                    bool nodeIdCheck = (prevNodeInfo.ID < currentNodeInfo.ID) ? true : false;



                    // 1.예외 처리 - > 만약에 현재 연결할 노드 (자식)가 다른 부모를 가진 경우( 리프 노드가 아닌 경우 ) -> 연결을 허용해야 하는가..? 
                    // 허용한 다면 체크하는 방법은 -> 연결할 노드 (자식)의 라인렌더러 오브젝트를 가지고 있는 지 확인하면 된다. 
                    // 연결할 노드 (자식)의 라인렌더러 오브젝트가 존재한다면 -> 리프 노드가 아님. 

                    // 2. 예외 처리 - > FindGetHeight(MindMapNodeInfo root) 함수를 통해 마인드 맵의 깊이를 확인
                    // 깊이가 같은 노드들끼리는 연결할 수 없도록 설정. 



                    if (nodeIdCheck)
                    {
                        // 이전에 생성한 노드 ID 가 낮은 경우 -> 이전에 생성한 노드(부모),  현재 연결할 노드 노드(자식) 
                        // 이 경우에만 부모 - 자식 관계가 형성될 수 있다.

                        print("ID 값 확인 : " + "이전에 생성한 노드 정보 : " + prevNodeInfo.ID + "  " + "현재 연결할 노드 정보 : " + currentNodeInfo.ID);

                        // "링크"를 허공에 끌어 당기고, 연결할 노드를 찾으면 노드가 연결된다. ( 부모 - 자식) 
                        prevConnectionNode.ConnectionNode(currentConnectionNode.transform.gameObject, false);

                        // prevConnectionNode(부모) 자식 노드 리스트 Children에 currentConnectionNode (자식) 노드를 추가한다. 
                        prevNodeInfo.Children.Add(currentNodeInfo);

                    }
                    else
                    {
                        // 이전에 생성한 노드 ID 가 높은 경우 단순히 연결만 지원한다. (양 방향 링크 지원)
                        // 제한 사항 -> 자식에서 부모로 연결할 수 없다. 
                        currentConnectionNode.ConnectionNode(prevConnectionNode.transform.gameObject, false);

                    }

                    //  "링크"를 허공에 끌어 당기고, 연결할 노드를 찾지 못하면 "링크" 가 사라진다. 
                    prevConnectionNode.OnDestroyIndexTip(R_indexTip);

                    rightIndexTip = true;
                }
            }

        }

    }


    // 사용자가 마인드 노드에 텍스트를 입력하는 메소드
    private void UpdateInputText()
    {

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
    public void UpdateDelete()
    {
        // 키보드 V번 키를 누르면 선택된 마인드 노드를 확인하고 삭제.
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        GameObject deleteNode = hover;
        MindMapNodeInfo deleteNodeInfo;

        if (deleteNode != null)
        {
            deleteNodeInfo = deleteNode.GetComponentInChildren<MindMapNodeInfo>();
            PlayerInfo.instance.cursorObject.GetComponent<XR_Bubble>().OffButtonMind();
            // 1. 리프 노드를 삭제하는 경우는 해당 리프 노드를 찾아서 삭제한다. 
            if (deleteNodeInfo.Children.Count == 0)
            {
                // 1. 연결되어 있는 노드 렌더러들을 모두 삭제.                 
                ConnectionNodeController.destroyLineRenderer(deleteNode);

                // 2. 해당 삭제할 노드 삭제 
                Destroy(deleteNode);

                // 3. 노드 삭제 SFX 사운드 실행 
                SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_DELETE);
            }
            else
            {
                // 2. 중간 노드를 삭제하는 경우는 서브트리 전체를 삭제 한다.

                MindMapNodeInfo nodeInfo = hover.GetComponentInChildren<MindMapNodeInfo>();
                if (nodeInfo != null)
                {

                    MindMapManager.instance.DeleteSubTree(nodeInfo);
                }
                // 3. 노드 삭제 SFX 사운드 실행 
                SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_DELETE);
            }
        }
        //}
    }

    // 마인드 노드 선택하는 메소드 
    private void UpdateSeleted()
    {
        // 마인드 선택 
        // 검지의 TIP을 구체 POKE를 하면 마인드 선택 상태가 되면 마인드 UI가 실행된다.

        // 서브
    }


    // 마인드 노드를 생성하는 메소드 
    public void UpdateCreate(Transform attchTransform, string value)
    {

        // z번 키를 누르면 사용자 R_indexTip의 위치에서 노드가 생성된다. (QA 완료 )
        if (PlayerInfo.instance.createBubble)
        {
            PlayerInfo.instance.createBubble = false;
            Debug.Log("Hello");
            GameObject obj = Resources.Load<GameObject>("Prefabs/Bubble");
            GameObject CreateNode = Instantiate(obj, attchTransform.position, Quaternion.identity);

            // 노드 생성시 생성 SFX  사운드 실행 
            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_CREATE);

            //TextMeshProUGUI nodeText = CreateNode.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

            // 생성된 노드들은 [ MindMapManager ]의 하위에 저장된다,
            CreateNode.transform.SetParent(mindNodeManager.transform);

            // 생성된 노드의 정보에 접근한다. 
            nodeInfo = CreateNode.GetComponentInChildren<MindMapNodeInfo>();

            if (nodeInfo.ID == 0)
            {
                // 오브젝트 이름을 루트 노드로 변경. 
                CreateNode.name = "RootNode";

                // 임시로 노드의 더미 데이터 삽입.
                nodeInfo.DATA = value;
                CreateNode.GetComponent<XR_Bubble>().mindText.text = nodeInfo.DATA;
                //nodeText.text = "RootNode";

                // 루트 노드(주제)로 지정한다. 
                nodeInfo.ROOTNODE = true;

                // MindeMapManager의 트리 루트로 설정.
                MindMapManager.instance.ROOTNODE = nodeInfo;
            }
            else
            {
                // 오브젝트 이름을 자식 노드로 변경.
                CreateNode.name = "ChildNode_" + nodeInfo.ID;

                // 임시로 노드의 더미 데이터 삽입.
                //nodeInfo.DATA = "ChildNode_" + nodeInfo.ID;
                nodeInfo.DATA = value;
                CreateNode.GetComponent<XR_Bubble>().mindText.text = nodeInfo.DATA;

                //nodeText.text = "ChildNode_" + nodeInfo.ID;
            }


        }

    }
}