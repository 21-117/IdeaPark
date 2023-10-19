using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeInfo;

public class MindMapTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // MindMap Ŭ������ MakeMindMapTree �޼��带 ȣ���Ͽ� ���ε� ���� ����ϴ�.
        MindMapNode root = MindMap.Get().MakeMindMapTree();

        // Root ��� ��ü DATA �� ��� 
        MindMap.instance.RootPrintTree(root);

        // �� ����� ��ü ID ���� ���
       //Debug.Log("���ε� �� ��ü ID: ���" + MindMap.instance.GetID);

        // ������� ���ε� ���� ����մϴ�.
        //Debug.Log("���ε� ��: ���");
        MindMap.instance.PrintTree(root);

        // ���ε� ���� ���̸� ������ ����մϴ�.
        int height = MindMap.instance.GetHeight(root);
        //Debug.Log("���ε� �� ����: " + height);

    }

    
}
