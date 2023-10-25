using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ������Ʈ�� ��ü���� ������ ������ �ִ� Ŭ���� 
public class MindMapNodeInfo : MonoBehaviour 
{
    private bool rootNode; 
    private int id = 0; // ����� ID 
    private string data = "";  // �ؽ�Ʈ ����. 
    private int type; // ����� ���� ���� Ÿ�� [Cube, Sphere, Capsule, Cylinder] 
    private Transform tr; // ����� ���� ��ġ 
    private Color color; // �� ����.
    private string aiData; // AI Ű���� ��õ ����. 
    public bool isSelected; // ���� ����.

    // �ڽ� ���ε�� ��忡 ���� ������ ����Ʈ ���� ����. 
    public List<MindMapNodeInfo> Children = new List<MindMapNodeInfo>();

    public void UpdateNodeId()
    {
        this.id = MindMapManager.instance.GetID;
        // �� ����� ID ���� ���

        // ��尡 ������ �� ���� ID ���� 1�� �����Ѵ�.
        MindMapManager.instance.GetID += 1;
    }

    // ID ������ ���� ���� ������Ƽ
    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    // �ؽ�Ʈ ������ ���� ���� ������Ƽ
    public string DATA
    {
        get { return data; }
        set { data = value; }
    }

    // ����� ���� ��ġ�� ���� ���� ������Ƽ
    public Transform POSITION
    {
        get { return tr; }
        set { tr = value; }
    }

    // ���� ���� ���� ������Ƽ
    public Color COLOR
    {
        get { return color; }
        set { color = value; }
    }

    // AI Ű���� ��õ ������ ���� ���� ������Ƽ
    public string AIDATA
    {
        get { return aiData; }
        set { aiData = value; }
    }

    // ����� ������ ��Ʈ ��忡 ���� ���� ������Ƽ
    public bool ROOTNODE
    {
        get { return rootNode; }
        set { rootNode = value; }
    }



}
