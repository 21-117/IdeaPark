using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MindMapController : MonoBehaviour
{
    // ���ε�� CRUD  ��� ��Ʈ�ѷ�

    [HideInInspector]
    public Transform R_indexTip;

    // R_indexTip ���� ����
    private bool indexTip = true;

    // ��ġ ���� 
    private bool isPinched;

    // ��ġ�� ������Ʈ  
    private GameObject target = null;

    // ��ġ�� target ������Ʈ CreataNodeConnection ��ũ��Ʈ
    CreataNodeConnection tempNodeCon;

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
        // MindMap�� �����ϴ� �̱��� Ŭ������ �����Ѵ�. 
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

    // ���ε� ��带 ��ũ�ϰ�, Ʈ���� �����ϴ� �޼ҵ� 
    // �ڽ� ���� ������ ����.
    // ��Ʈ ����� �ڽ����� ����. 
    // ���� -> �ڽ��� �θ𺸴� id ���� Ŭ���� ����(����ó��)
    private void UpdateConnection()
    {
        //  ���ε� ����(��ũ)
        // "�������� ���� ���ε�"�� PICH�ϸ� ��ũ�� Ȱ��ȭ �ȴ�.
        if (isPinched && isHovered)
        {
            if (indexTip)
            {
                tempNodeCon = target.GetComponentInChildren<CreataNodeConnection>();
                //Ȱ��ȭ�� "��ũ"�� ����� ���� ���� �ڵ����� ���ε尡 �����Ǹ�, �ڵ����� �ؽ�Ʈ �Է� ���°��ȴ�.
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

    // ����ڰ� ���ε� ��忡 �ؽ�Ʈ�� �Է��ϴ� �޼ҵ�
    private void UpdateInputText()
    {
        print("�ؽ�Ʈ �Է�â ����,");
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
        // ���ε� ���� 
        // "���ε� ���� ���¿��� ����� "���ε� UI "�� DELETE�� ������ �ش� ���ε带 ����
        // Ű���� 2�� Ű�� ������ ���õ� ���ε� ��带 Ȯ���ϰ� ����. 
    }

    // ���ε� ��� �����ϴ� �޼ҵ� 
    private void UpdateSeleted()
    {
        // ���ε� ���� 
        // ������ TIP�� ��ü POKE�� �ϸ� ���ε� ���� ���°� �Ǹ� ���ε� UI�� ����ȴ�.
    }

    // ���ε� ��带 �����ϴ� �޼ҵ� 
    private void UpdateCreate()
    {

            GameObject obj = Resources.Load<GameObject>("Bubble");
            GameObject node = Instantiate(obj, R_indexTip.position, Quaternion.identity);

            if (node.GetComponentInChildren<MindMapNodeInfo>().ID == 1)
            {
                // ��Ʈ ���(����)�� �����Ѵ�. 
                node.GetComponentInChildren<MindMapNodeInfo>().ROOTNODE = true;
            }

            //state = State.CONNECTION;


    }
}
