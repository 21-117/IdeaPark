using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MindMapManager;

public class MindMapTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // MindMap Ŭ������ MakeMindMapTree �޼��带 ȣ���Ͽ� ���ε� ���� ����ϴ�.
        MindMapNode root = MindMap.MakeMindMapTree();

        // ������� ���ε� ���� ����մϴ�.
        Debug.Log("���ε� ��: ���");
        MindMap.PrintTree(root);

        // ���ε� ���� ���̸� ������ ����մϴ�.
        int height = MindMap.GetHeight(root);
        Debug.Log("���ε� �� ����: " + height);

    }

    
}
