using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class MindMapController : MonoBehaviour
{
    public static MindMapController mindMapController;

    // ���ε�� ������ ��� ��Ʈ�ѷ�
    public GameObject R_indexTip;

    // ���ε�� ������ �θ�� �����ϴ� ������Ʈ
    private GameObject mindNodeManager;

    // Hover�� ������Ʈ 
    private GameObject hover = null;

    // ����ڰ� ������ ����� Info ����  
    private MindMapNodeInfo nodeInfo;

    // ���ŵ� ���� ����� ���η����� controller 
    private ConnectionNodeController currentConnectionNode = null;

    // ���� ����� �ӽ� ���η����� controller 
    private ConnectionNodeController prevConnectionNode;

    // R_indexTip ��� ����
    private bool rightIndexTip = true;

    // ����� ������ ���� ������Ʈ 
    private bool upDateData;

    // XR_Ray �� �� ����ġ ����ġ ����ġ ����
    public bool _nodeAttach = false;
    public bool _nodeDetach = false;
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

    // ���ε� ��� ���� �޼ҵ� ȣ��
    public static Action<Vector3, string> CreateMindNode;

    // �ӽ÷� ����. 
    private int count = 0;


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


    void Start()
    {

        mindNodeManager = GameObject.Find("[ MINDNODE MANAGER ]");

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

        //CreateMindNode = (pos, node) =>
        //{
        //    UpdateCreate(pos, node);
        //};
    }

    // Update is called once per frame
    void Update()
    {
        // 1�� Ű�� ������ �÷��̾ ��带 ������ �� �ִ� CREATE
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            print("1�� Ű�� ������. ���ε� �� ��� ����. ");
            UpdateCreate(R_indexTip.transform.position, "");
        }

        // 2�� Ű�� ������ �÷��̾ ��带 ������ �� �ִ� CONNECTION


        // 3�� Ű�� ������ �÷��̾ ��忡 �����͸� ����


        // 4�� Ű�� ������ ���� ������ ��� ����� �����͸� ���.
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // ��Ʈ ��带 ã�Ƽ� ��ȯ 
            MindMapNodeInfo root = MindMapTreeManager.instance.RootFindTree();
            MindMapTreeManager.instance.PrintTree(root);
        }

        //// 5�� Ű�� ������ ���� Hover�� ����� �ڽ� ����Ʈ �����͸� ���
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    MindMapNodeInfo currentNodeInfo = hover.GetComponentInChildren<MindMapNodeInfo>();

        //    if (currentNodeInfo != null)
        //    {


        //    }
        //}
    }

    // ���ε� ��带 ��ũ�ϰ�, Ʈ���� �����ϴ� �޼ҵ� 
    //public void UpdateConnection()
    //{
    //    if (PlayerInfo.localPlayer.rayObject != null)
    //    {
    //        currentConnectionNode = PlayerInfo.localPlayer.rayObject.GetComponentInChildren<ConnectionNodeController>();

    //        if (_nodeAttach)
    //        {

    //            if (rightIndexTip)
    //            {
    //                currentConnectionNode.ConnectionNode(R_indexTip, true);
    //                prevConnectionNode = currentConnectionNode;
    //                rightIndexTip = false;
    //            }
    //        }

    //        if (_nodeDetach)
    //        {

    //            if (!rightIndexTip)
    //            {
    //                MindMapNodeInfo prevNodeInfo = prevConnectionNode.GetComponent<MindMapNodeInfo>();
    //                MindMapNodeInfo currentNodeInfo = currentConnectionNode.GetComponent<MindMapNodeInfo>();

    //                //// ������ �ӽ� ���� ��� ����, ���� ������ ��� ���� : ID ���� ���Ѵ�. 
    //                bool nodeIdCheck = (prevNodeInfo.ID < currentNodeInfo.ID) ? true : false;

    //                // 1.���� ó�� - > ���࿡ ���� ������ ��� (�ڽ�)�� �ٸ� �θ� ���� ���( ���� ��尡 �ƴ� ��� ) -> ������ ����ؾ� �ϴ°�..? 
    //                // ����� �ٸ� üũ�ϴ� ����� -> ������ ��� (�ڽ�)�� ���η����� ������Ʈ�� ������ �ִ� �� Ȯ���ϸ� �ȴ�. 
    //                // ������ ��� (�ڽ�)�� ���η����� ������Ʈ�� �����Ѵٸ� -> ���� ��尡 �ƴ�. 

    //                // 2. ���� ó�� - > FindGetHeight(MindMapNodeInfo root) �Լ��� ���� ���ε� ���� ���̸� Ȯ��
    //                // ���̰� ���� ���鳢���� ������ �� ������ ����. 



    //                if (nodeIdCheck)
    //                {
    //                    // ������ ������ ��� ID �� ���� ��� -> ������ ������ ���(�θ�),  ���� ������ ��� ���(�ڽ�) 
    //                    // �� ��쿡�� �θ� - �ڽ� ���谡 ������ �� �ִ�.

    //                    print("ID �� Ȯ�� : " + "������ ������ ��� ���� : " + prevNodeInfo.ID + "  " + "���� ������ ��� ���� : " + currentNodeInfo.ID);

    //                    // "��ũ"�� ����� ���� ����, ������ ��带 ã���� ��尡 ����ȴ�. ( �θ� - �ڽ�) 
    //                    prevConnectionNode.ConnectionNode(currentConnectionNode.transform.gameObject, false);

    //                    // prevConnectionNode(�θ�) �ڽ� ��� ����Ʈ Children�� currentConnectionNode (�ڽ�) ��带 �߰��Ѵ�. 
    //                    prevNodeInfo.Children.Add(currentNodeInfo);

    //                }
    //                else
    //                {
    //                    // ������ ������ ��� ID �� ���� ��� �ܼ��� ���Ḹ �����Ѵ�. (�� ���� ��ũ ����)
    //                    // ���� ���� -> �ڽĿ��� �θ�� ������ �� ����. 
    //                    currentConnectionNode.ConnectionNode(prevConnectionNode.transform.gameObject, false);

    //                }

    //                //  "��ũ"�� ����� ���� ����, ������ ��带 ã�� ���ϸ� "��ũ" �� �������. 
    //                prevConnectionNode.OnDestroyIndexTip(R_indexTip);

    //                rightIndexTip = true;
    //            }
    //        }


    //    }

    //}


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

    //public void UpdateDelete()
    //{

    //    GameObject deleteNode = PlayerInfo.localPlayer.GrabObject;
    //    MindMapNodeInfo deleteNodeInfo;

    //    if (deleteNode != null)
    //    {

    //        deleteNodeInfo = deleteNode.GetComponentInChildren<MindMapNodeInfo>();

    //        // ������ ����� ������ 1��, 2������ �ذ��� �� �ֳ�,,,? 
    //        print("���� �����ҷ��� ����� ����Ʈ�� ���� Ȯ��. : " + deleteNodeInfo.Children.Count);


    //        // 1. ���� ��带 �����ϴ� ���� �ش� ���� ��带 ã�Ƽ� �����Ѵ�. 
    //        if (deleteNodeInfo.Children.Count == 0)
    //        {
    //            // 1. ����Ǿ� �ִ� ��� ���������� ��� ����.                 
    //            ConnectionNodeController.destroyLineRenderer(deleteNode);

    //            // 2. �ش� ������ ��� ���� 
    //            Destroy(deleteNode);

    //            // 3. ��� ���� SFX ���� ���� 
    //            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_DELETE);
    //        }
    //        else
    //        {
    //            // 2. �߰� ��带 �����ϴ� ���� ����Ʈ�� ��ü�� ���� �Ѵ�.

    //            MindMapNodeInfo nodeInfo = hover.GetComponentInChildren<MindMapNodeInfo>();
    //            if (nodeInfo != null)
    //            {
    //                // 3. �ش� ����� ����Ʈ���� ��� ã�Ƽ� ����. 
    //                MindMapTreeManager.instance.DeleteSubTree(nodeInfo);
    //            }
    //            // 4. ��� ���� SFX ���� ���� 
    //            SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_DELETE);
    //        }
    //    }

    //}


    // ���ε� ��带 �����ϴ� �޼ҵ� 
    public void UpdateCreate(Vector3 attachPos, string value)
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/Bubble_New");

        GameObject CreateNode = Instantiate(obj, attachPos, Quaternion.identity);
        CreateNode.name = "Bubble_" + count.ToString();

        // ��� ������ ���� SFX  ���� ���� 
        //SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_CREATE);

        // ������ ������ [ MindMapManager ]�� ������ ����ȴ�,
        CreateNode.transform.SetParent(mindNodeManager.transform);

        // ������ ����� ������ ���Ѵ�. 
        nodeInfo = CreateNode.GetComponentInChildren<MindMapNodeInfo>();


        //if (PlayerInfo.localPlayer.createBubble)
        //{
        //    PlayerInfo.localPlayer.createBubble = false;
        //    GameObject CreateNode = PhotonNetwork.Instantiate("Prefabs/Bubble", attachPos, Quaternion.identity);
        //    CreateNode.name = "Bubble_" + count.ToString();

        //    // ��� ������ ���� SFX  ���� ���� 
        //    SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_NODE_CREATE);

        //    // ������ ������ [ MindMapManager ]�� ������ ����ȴ�,
        //    CreateNode.transform.SetParent(mindNodeManager.transform);

        //    // ������ ����� ������ ���Ѵ�. 
        //    nodeInfo = CreateNode.GetComponentInChildren<MindMapNodeInfo>();


        //    CreateNode.GetComponent<XR_Bubble>().mindText.text = value;
        //    //if (nodeInfo.ID == 0)
        //    //{
        //    //    // ������Ʈ �̸��� ��Ʈ ���� ����. 
        //    //    CreateNode.name = "RootNode";

        //    //    // �ӽ÷� ����� ���� ������ ����.
        //    //    //nodeInfo.DATA = value;
        //    //    nodeInfo.DATA = "RootNode" + nodeInfo.ID.ToString();
        //    //    CreateNode.GetComponent<XR_Bubble>().mindText.text = nodeInfo.DATA;

        //    //    // ��Ʈ ���(����)�� �����Ѵ�. 
        //    //    nodeInfo.ROOTNODE = true;

        //    //    // MindeMapManager�� Ʈ�� ��Ʈ�� ����.
        //    //    MindMapTreeManager.instance.ROOTNODE = nodeInfo;
        //    //}
        //    //else
        //    //{
        //    //    // ������Ʈ �̸��� �ڽ� ���� ����.
        //    //    CreateNode.name = "ChildNode_" + nodeInfo.ID;

        //    //    // �ڽ� ����� ������ ����.
        //    //    //nodeInfo.DATA = value;
        //    //    nodeInfo.DATA = "Child" + nodeInfo.ID.ToString();

        //    //    // �ڽ� ����� Text. 
        //    //    CreateNode.GetComponent<XR_Bubble>().mindText.text = nodeInfo.DATA;


        //    //}

        //    count++;
        //}

    }
}