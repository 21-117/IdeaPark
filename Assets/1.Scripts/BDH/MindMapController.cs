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

    // ���ε�� ������ ��� ��Ʈ�ѷ�
    public GameObject R_indexTip;

    // ���ε�� ���� ��� ��Ʈ�ѷ�
    public GameObject L_indexTip;

    // ���ε�� ������ �θ�� �����ϴ� ������Ʈ
    private GameObject mindNodeManager;

    // Hover�� ������Ʈ 
    private GameObject hover = null;

    // ����ڰ� ������ ����� Info ����  
    private MindMapNodeInfo nodeInfo;

    // ���ŵ� ���� ����� ���η����� controller 
    public ConnectionNodeController currentConnectionNode = null;

    // ���� ����� �ӽ� ���η����� controller 
    private ConnectionNodeController prevConnectionNode;

    // R_indexTip ��� ����
    private bool rightIndexTip = true;

    // ����� ������ ���� ������Ʈ 
    private bool upDateData;

    // ��ġ ���� 
    private bool isPinched;

    // ��ġ�� ������Ʈ  
    private GameObject pinch = null;

    // Hover ����
    private bool isHovered;

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
        set { isPoked = value; }
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
        //���η������� ����, ���ε���� �θ� ���� �ڽ� ��带 ����. 
        if (!rightIndexTip)
        {
            MindMapNodeInfo prevNodeInfo = prevConnectionNode.GetComponent<MindMapNodeInfo>();
            MindMapNodeInfo currentNodeInfo = currentConnectionNode.GetComponent<MindMapNodeInfo>();

            //// ������ �ӽ� ���� ��� ����, ���� ������ ��� ���� : ID ���� ���Ѵ�. 
            bool nodeIdCheck = (prevNodeInfo.ID < currentNodeInfo.ID) ? true : false;



            // 1.���� ó�� - > ���࿡ ���� ������ ��� (�ڽ�)�� �ٸ� �θ� ���� ���( ���� ��尡 �ƴ� ��� ) -> ������ ����ؾ� �ϴ°�..? 
            // ����� �ٸ� üũ�ϴ� ����� -> ������ ��� (�ڽ�)�� ���η����� ������Ʈ�� ������ �ִ� �� Ȯ���ϸ� �ȴ�. 
            // ������ ��� (�ڽ�)�� ���η����� ������Ʈ�� �����Ѵٸ� -> ���� ��尡 �ƴ�. 

            // 2. ���� ó�� - > FindGetHeight(MindMapNodeInfo root) �Լ��� ���� ���ε� ���� ���̸� Ȯ��
            // ���̰� ���� ���鳢���� ������ �� ������ ����. 



            if (nodeIdCheck)
            {
                // ������ ������ ��� ID �� ���� ��� -> ������ ������ ���(�θ�),  ���� ������ ��� ���(�ڽ�) 
                // �� ��쿡�� �θ� - �ڽ� ���谡 ������ �� �ִ�.

                print("ID �� Ȯ�� : " + "������ ������ ��� ���� : " + prevNodeInfo.ID + "  " + "���� ������ ��� ���� : " + currentNodeInfo.ID);

                // "��ũ"�� ����� ���� ����, ������ ��带 ã���� ��尡 ����ȴ�. ( �θ� - �ڽ�) 
                prevConnectionNode.ConnectionNode(currentConnectionNode.transform.gameObject, false);

                // prevConnectionNode(�θ�) �ڽ� ��� ����Ʈ Children�� currentConnectionNode (�ڽ�) ��带 �߰��Ѵ�. 
                prevNodeInfo.Children.Add(currentNodeInfo);

            }
            else
            {
                // ������ ������ ��� ID �� ���� ��� �ܼ��� ���Ḹ �����Ѵ�. (�� ���� ��ũ ����)
                // ���� ���� -> �ڽĿ��� �θ�� ������ �� ����. 
                currentConnectionNode.ConnectionNode(prevConnectionNode.transform.gameObject, false);

            }

            //  "��ũ"�� ����� ���� ����, ������ ��带 ã�� ���ϸ� "��ũ" �� �������. 
            prevConnectionNode.OnDestroyIndexTip(R_indexTip);

            rightIndexTip = true;
        }
    }

    // ���ε� ��带 ��ũ�ϰ�, Ʈ���� �����ϴ� �޼ҵ� 
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

                    //// ������ �ӽ� ���� ��� ����, ���� ������ ��� ���� : ID ���� ���Ѵ�. 
                    bool nodeIdCheck = (prevNodeInfo.ID < currentNodeInfo.ID) ? true : false;



                    // 1.���� ó�� - > ���࿡ ���� ������ ��� (�ڽ�)�� �ٸ� �θ� ���� ���( ���� ��尡 �ƴ� ��� ) -> ������ ����ؾ� �ϴ°�..? 
                    // ����� �ٸ� üũ�ϴ� ����� -> ������ ��� (�ڽ�)�� ���η����� ������Ʈ�� ������ �ִ� �� Ȯ���ϸ� �ȴ�. 
                    // ������ ��� (�ڽ�)�� ���η����� ������Ʈ�� �����Ѵٸ� -> ���� ��尡 �ƴ�. 

                    // 2. ���� ó�� - > FindGetHeight(MindMapNodeInfo root) �Լ��� ���� ���ε� ���� ���̸� Ȯ��
                    // ���̰� ���� ���鳢���� ������ �� ������ ����. 



                    if (nodeIdCheck)
                    {
                        // ������ ������ ��� ID �� ���� ��� -> ������ ������ ���(�θ�),  ���� ������ ��� ���(�ڽ�) 
                        // �� ��쿡�� �θ� - �ڽ� ���谡 ������ �� �ִ�.

                        print("ID �� Ȯ�� : " + "������ ������ ��� ���� : " + prevNodeInfo.ID + "  " + "���� ������ ��� ���� : " + currentNodeInfo.ID);

                        // "��ũ"�� ����� ���� ����, ������ ��带 ã���� ��尡 ����ȴ�. ( �θ� - �ڽ�) 
                        prevConnectionNode.ConnectionNode(currentConnectionNode.transform.gameObject, false);

                        // prevConnectionNode(�θ�) �ڽ� ��� ����Ʈ Children�� currentConnectionNode (�ڽ�) ��带 �߰��Ѵ�. 
                        prevNodeInfo.Children.Add(currentNodeInfo);

                    }
                    else
                    {
                        // ������ ������ ��� ID �� ���� ��� �ܼ��� ���Ḹ �����Ѵ�. (�� ���� ��ũ ����)
                        // ���� ���� -> �ڽĿ��� �θ�� ������ �� ����. 
                        currentConnectionNode.ConnectionNode(prevConnectionNode.transform.gameObject, false);

                    }

                    //  "��ũ"�� ����� ���� ����, ������ ��带 ã�� ���ϸ� "��ũ" �� �������. 
                    prevConnectionNode.OnDestroyIndexTip(R_indexTip);

                    rightIndexTip = true;
                }
            }

        }

    }


    // ����ڰ� ���ε� ��忡 �ؽ�Ʈ�� �Է��ϴ� �޼ҵ�
    private void UpdateInputText()
    {

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
    public void UpdateDelete()
    {
        // Ű���� V�� Ű�� ������ ���õ� ���ε� ��带 Ȯ���ϰ� ����.
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        GameObject deleteNode = hover;
        MindMapNodeInfo deleteNodeInfo;

        if (deleteNode != null)
        {
            deleteNodeInfo = deleteNode.GetComponentInChildren<MindMapNodeInfo>();
            PlayerInfo.instance.cursorObject.GetComponent<XR_Bubble>().OffButtonMind();
            // 1. ���� ��带 �����ϴ� ���� �ش� ���� ��带 ã�Ƽ� �����Ѵ�. 
            if (deleteNodeInfo.Children.Count == 0)
            {
                // 1. ����Ǿ� �ִ� ��� ���������� ��� ����.                 
                ConnectionNodeController.destroyLineRenderer(deleteNode);

                // 2. �ش� ������ ��� ���� 
                Destroy(deleteNode);

                // 3. ��� ���� SFX ���� ���� 
                SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_DELETE);
            }
            else
            {
                // 2. �߰� ��带 �����ϴ� ���� ����Ʈ�� ��ü�� ���� �Ѵ�.

                MindMapNodeInfo nodeInfo = hover.GetComponentInChildren<MindMapNodeInfo>();
                if (nodeInfo != null)
                {

                    MindMapManager.instance.DeleteSubTree(nodeInfo);
                }
                // 3. ��� ���� SFX ���� ���� 
                SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_DELETE);
            }
        }
        //}
    }

    // ���ε� ��� �����ϴ� �޼ҵ� 
    private void UpdateSeleted()
    {
        // ���ε� ���� 
        // ������ TIP�� ��ü POKE�� �ϸ� ���ε� ���� ���°� �Ǹ� ���ε� UI�� ����ȴ�.

        // ����
    }


    // ���ε� ��带 �����ϴ� �޼ҵ� 
    public void UpdateCreate(Transform attchTransform, string value)
    {

        // z�� Ű�� ������ ����� R_indexTip�� ��ġ���� ��尡 �����ȴ�. (QA �Ϸ� )
        if (PlayerInfo.instance.createBubble)
        {
            PlayerInfo.instance.createBubble = false;
            Debug.Log("Hello");
            GameObject obj = Resources.Load<GameObject>("Prefabs/Bubble");
            GameObject CreateNode = Instantiate(obj, attchTransform.position, Quaternion.identity);

            // ��� ������ ���� SFX  ���� ���� 
            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_CREATE);

            //TextMeshProUGUI nodeText = CreateNode.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

            // ������ ������ [ MindMapManager ]�� ������ ����ȴ�,
            CreateNode.transform.SetParent(mindNodeManager.transform);

            // ������ ����� ������ �����Ѵ�. 
            nodeInfo = CreateNode.GetComponentInChildren<MindMapNodeInfo>();

            if (nodeInfo.ID == 0)
            {
                // ������Ʈ �̸��� ��Ʈ ���� ����. 
                CreateNode.name = "RootNode";

                // �ӽ÷� ����� ���� ������ ����.
                nodeInfo.DATA = value;
                CreateNode.GetComponent<XR_Bubble>().mindText.text = nodeInfo.DATA;
                //nodeText.text = "RootNode";

                // ��Ʈ ���(����)�� �����Ѵ�. 
                nodeInfo.ROOTNODE = true;

                // MindeMapManager�� Ʈ�� ��Ʈ�� ����.
                MindMapManager.instance.ROOTNODE = nodeInfo;
            }
            else
            {
                // ������Ʈ �̸��� �ڽ� ���� ����.
                CreateNode.name = "ChildNode_" + nodeInfo.ID;

                // �ӽ÷� ����� ���� ������ ����.
                //nodeInfo.DATA = "ChildNode_" + nodeInfo.ID;
                nodeInfo.DATA = value;
                CreateNode.GetComponent<XR_Bubble>().mindText.text = nodeInfo.DATA;

                //nodeText.text = "ChildNode_" + nodeInfo.ID;
            }


        }

    }
}