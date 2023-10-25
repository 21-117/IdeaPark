using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class MindMapController : MonoBehaviour
{
    // ���ε�� ������ ��� ��Ʈ�ѷ�
    public GameObject R_indexTip;

    // ���ε�� ���� ��� ��Ʈ�ѷ�
    public GameObject L_indexTip;

    // ���ε�� ������ �θ�� �����ϴ� ������Ʈ
    public GameObject nodeManager;

    // ����ڰ� ������ ����� Info ���� ( ������ ������ ����)  
    private MindMapNodeInfo nodeInfo;  

    // R_indexTip ��� ����
    private bool rightIndexTip = true;

    // ����� ������ ���� ������Ʈ 
    private bool upDateData; 

    // ����� ������ ��ġ�� �ӽ÷� ���� 
    CreateNodeContoller ConnectionPointNode;

    // ��ġ ���� 
    private bool isPinched;

    // ��ġ�� ������Ʈ  
    private GameObject pinch = null;

    // Hover ����
    private bool isHovered;

    // Hover�� ������Ʈ 
    private GameObject hover = null;

    // Poke ����
    private bool isPoked;

    // Poke�� ������Ʈ
    private GameObject poke = null;

    // ��ġ�� ���� ����
    public static Action<bool, GameObject> setPinch;

    // Hover�� ���� ���� 
    public static Action<bool, GameObject> setHover;

    // POKE�� ���� ����
    public static Action<bool, GameObject> setPoke; 

    // Poke�� ���� ������Ƽ
    public bool ISPOKED
    {
        get { return isPoked; }
        set { isPoked = value;  }
    }

    // ��ġ�� ���� ������Ƽ
    public bool ISPINCHED
    {
        get { return isPinched; }
        set { isPinched = value; }
    }

    // Hover�� ���� ������Ƽ
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

        
        // 1�� Ű�� ������ �÷��̾ ��带 ������ �� �ִ� CREATE
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            state = State.CREATE;
        }

        // 2�� Ű�� ������ �÷��̾ ��带 ������ �� �ִ� CONNECTION
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            state = State.CONNECTION;
        }

        // 3�� Ű�� ������ �÷��̾ ��忡 �����͸� ����
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            state = State.DELETE;
        }
    }

    // ���ε� ��带 ��ũ�ϰ�, Ʈ���� �����ϴ� �޼ҵ� 
    // �ڽ� ���� ������ ����.
    // ��Ʈ ����� �ڽ����� ����. 
    // ���� -> �ڽ��� �θ𺸴� id ���� Ŭ���� ����(����ó��)



    private void UpdateConnection()
    {
       
        if (hover != null)
        {
            print("���� �հ����� ���� ������Ʈ : " + hover.GetComponentInChildren<MindMapNodeInfo>().ID);

            CreateNodeContoller updateConnectionNode  = hover.GetComponentInChildren<CreateNodeContoller>();


            // X�� Ű�� ������ Ȱ��ȭ�� "��ũ"�� ����� ���� ���� �ڵ����� R_indexTip�� ��ġ�� ���η������� �����. (QA �Ϸ�)
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (rightIndexTip)
                {
                    updateConnectionNode.ConnectionIndexNode(R_indexTip, true);
                    ConnectionPointNode = updateConnectionNode;
                    rightIndexTip = false;
                }
               
            }

            // C�� Ű�� ������ ���η������� ����, ���ε���� �θ� ���� �ڽ� ��带 ����. 
            // 1. "��ũ"�� ����� ���� ����, ������ ��带 ã���� ��尡 ����ȴ�.
            // 2.  "��ũ"�� ����� ���� ����, ������ ��带 ã�� ���ϸ� "��ũ" �� �������. 
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!rightIndexTip)
                {
                    //// ����ڷ� ���� �Է¹޾� ������ ��� ����
                    //MindMapNodeInfo selectedInfo = hover.GetComponentInChildren<MindMapNodeInfo>();

                    //// ������ ��� ���� : nodeInfo, ������ ��� ���� : hover.GetComponentInChildren<MindMapNodeInfo>() �� ID ���� ���Ѵ�. 
                    //bool nodeCheck = (nodeInfo.ID > selectedInfo.ID) ? true : false;

                    //print("ID �� Ȯ�� : " + "NODEINFO.ID : " + nodeInfo.ID + "selectedInfo.ID : " + selectedInfo.ID);


                    //// ������ ��� ID �� ���� ��� -> CreateNode ������ ���(�θ�),  target�� ������ ���(�ڽ�) 
                    //if (nodeCheck)
                    //{

                    //    // CreateNode(�θ�) �ڽ� ��� ����Ʈ Children�� target (�ڽ�)�� �߰��Ѵ�. 
                    //    nodeInfo.Children.Add(selectedInfo);

                    //}
                    //// ������ ��� ID�� ���� ��� -> ������ ���(�θ�) , ������ ���(�ڽ�)
                    //else
                    //{
                    //    // target(�θ�) �ڽ� ��� ����Ʈ Children�� CreateNode (�ڽ�)�� �߰��Ѵ�.  
                    //    selectedInfo.Children.Add(nodeInfo);
                    //}

                    // ���ε�ʿ� ���� ��ũ�� ����. 
                    ConnectionPointNode.ConnectionIndexNode(updateConnectionNode.transform.gameObject, false);

                    //INDEXDISTAL�� ����� ���η����� ��带 ã�Ƽ� ����. (QA �Ϸ�)
                    ConnectionPointNode.OnDestroyindexObject();

                    rightIndexTip = true;
                }

            }

            
        }
 
    }

    // ����ڰ� ���ε� ��忡 �ؽ�Ʈ�� �Է��ϴ� �޼ҵ�
    private void UpdateInputText()
    {
        print("�ؽ�Ʈ �Է�â ���� : ��� ������ ��� : " + hover.GetComponentInChildren<MindMapNodeInfo>().DATA);

        // ���� �Է��Ϸ��� ��� 
        MindMapNodeInfo nodeInfo = hover.GetComponentInChildren<MindMapNodeInfo>();

        
       
    }

    // ������ ���� AIText ������ �޾� ó���ϴ� �޼ҵ� 
    private void UpdateAiText()
    {
        throw new NotImplementedException();
    }

    // ���ε� ����� ������ �����ϴ� �޼ҵ�. 
    private void UpdateColor()
    {
        throw new NotImplementedException();
    }

    // ���ε� ��带 �����ϴ� �޼ҵ� 
    private void UpdateDelete()
    {
     
        // Ű���� V�� Ű�� ������ ���õ� ���ε� ��带 Ȯ���ϰ� ����.
        
        if(Input.GetKeyDown(KeyCode.V)) {

            MindMapNodeInfo deleteNode = hover.GetComponentInChildren<MindMapNodeInfo>();
            CreateNodeContoller deleteNodeController = hover.GetComponentInChildren<CreateNodeContoller>();

            // 1. �߰� ��带 �����ϴ� ���� ����Ʈ�� ��ü�� ���� �Ѵ�.

            // 2. ���� ��带 �����ϴ� ���� �ش� ���� ��带 ã�Ƽ� �����Ѵ�. 
            if (deleteNode.Children.Count == 0)
            {
                print("��� ����");

                // 1. ����Ǿ� �ִ� ��� ���������� ��� ����.
               

                // 2. �ش� ������ ��� ���� 
                Destroy(deleteNode.transform.parent.gameObject);
            }
        }




    }

    // ���ε� ��� �����ϴ� �޼ҵ� 
    private void UpdateSeleted()
    {
        // ���ε� ���� 
        // ������ TIP�� ��ü POKE�� �ϸ� ���ε� ���� ���°� �Ǹ� ���ε� UI�� ����ȴ�.

        // ����
    }

    // ���ε� ��带 �����ϴ� �޼ҵ� 
    private void UpdateCreate()
    {

        // z�� Ű�� ������ ����� R_indexTip�� ��ġ���� ��尡 �����ȴ�. (QA �Ϸ� )
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/Test_Node");
            GameObject CreateNode = Instantiate(obj, R_indexTip.transform.position, Quaternion.identity);

            // ������ ������ [ MindMapManager ]�� ������ ����ȴ�,
            //CreateNode.transform.SetParent(NodeManager.transform);

            nodeInfo = CreateNode.GetComponentInChildren<MindMapNodeInfo>();

            // ����� ID ���� ����. 
            nodeInfo.UpdateNodeId();

            if (nodeInfo.ID == 0)
            {
                // ��Ʈ ���(����)�� �����Ѵ�. 
                nodeInfo.ROOTNODE = true;

                // MindeMapManager�� Ʈ�� ��Ʈ�� ����.
                MindMapManager.instance.ROOTNODE = nodeInfo; 
            }
           
        }

        


    }
}
