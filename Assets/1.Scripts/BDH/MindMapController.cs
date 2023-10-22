using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MindMapController : MonoBehaviour
{
    // ���ε�� CRUD  ��� ��Ʈ�ѷ�

    [HideInInspector]
    public Transform R_indexTip;

    // ��ġ ���� 
    private bool isPinched;

    // ��ġ�� ������Ʈ  
    private GameObject target = null;

    // Hover ����
    private bool isHovered;

    // Hover�� ������Ʈ 
    private GameObject hover = null;

    // ��ġ ����, ������Ʈ ����
    public static Action<bool, GameObject> setPinch;

    // Hover�� ������Ʈ ���� 
    public static Action<bool, GameObject> setHover;

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

        //  ���ε� ����(��ũ)
        // "�������� ���� ���ε�"�� PICH�ϸ� ��ũ�� Ȱ��ȭ �ȴ�.

       
        // �ڽ� ���� ������ ����.
        // ��Ʈ ����� �ڽ����� ����. 
        // ���� -> �ڽ��� �θ𺸴� id ���� Ŭ���� ����(����ó��)

       
        if (isPinched && isHovered)
        { 
            //Ȱ��ȭ�� "��ũ"�� ����� ���� ���� �ڵ����� ���ε尡 �����Ǹ�, �ڵ����� �ؽ�Ʈ �Է� ���°��ȴ�.
            CreataNodeConnection.connection(R_indexTip, true);

          

        }


       
        //if (hover != target)
        //{
           


        //    if (hover == target && isPinched)
        //    {
        //        print("�� ����� ������ �ȵǳ� ");
        //        CreataNodeConnection.destroyConnection();
        //    }
          
        //}

        // Target ������Ʈ�� ��带 ������ ����
        //CreataNodeConnection.connection(targetPos, false);






        //state = State.IDLE;
    }

    private void UpdateInputText()
    {
        print("�ؽ�Ʈ �Է�â ����,"); 
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
        // ���ε� ���� 
        // "���ε� ���� ���¿��� ����� "���ε� UI "�� DELETE�� ������ �ش� ���ε带 ����
        // Ű���� 2�� Ű�� ������ ���õ� ���ε� ��带 Ȯ���ϰ� ����. 
    }

    private void UpdateSeleted()
    {
        // ���ε� ���� 
        // ������ TIP�� ��ü POKE�� �ϸ� ���ε� ���� ���°� �Ǹ� ���ε� UI�� ����ȴ�.
    }

    private void UpdateCreate()
    {
       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            GameObject obj = Resources.Load<GameObject>("Prefabs/Test_Node");
            GameObject node = Instantiate(obj, R_indexTip.position, Quaternion.identity);

            if(node.GetComponentInChildren<MindMapNodeInfo>().ID == 1)
            {
                // ��Ʈ ���(����)�� �����Ѵ�. 
                node.GetComponentInChildren<MindMapNodeInfo>().ROOTNODE = true; 
            }

            state = State.CONNECTION;
        }


    }
}
