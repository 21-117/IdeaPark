using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindMapTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // MindMap Ŭ������ MakeMindMapTree �޼��带 ȣ���Ͽ� ���ε� ���� ����ϴ�.
        //MindMapNodeInfo root = MindMapManager.instance.MakeMindMapTree();

        // ���ε� �� "���� �ð�" ��带 ã�´�. 
        //MindMapNodeInfo tempNode = MindMapManager.instance.returnRootNode(root, "���� �ð�");

        // ���ε� �� "���� �ð�" ��� ����Ʈ���� ���
        // MindMapManager.instance.FindSubTree(tempNode);

        // ���ε� ���� root�� �ڽ� ����Ʈ�� DATA ��� 
        //MindMapManager.instance.PrintChildTree(root);



        // �� ����� ��ü ID ���� ���
        //Debug.Log("���ε� �� ��ü ID: ���" + MindMapManager.instance.GetID);

        // ������� ���ε� ���� ����մϴ�.
        //Debug.Log("���ε� ��: ���");
        //MindMapManager.instance.PrintTree(root);

        // ���ε� ���� ���̸� ������ ����մϴ�.
        //int height = MindMap.instance.GetHeight(root);
        //Debug.Log("���ε� �� ����: " + height);



        // ���� �׽�Ʈ �غ�����,,! 
        // ====================================================================

        // 1. ���ε� ���� ������ ��Ʈ ��带 ã�´�. 
        // ��Ʈ ��� ��ȯ �޼ҵ� - > ���ε� �� [ MindNodeManager ]�� ��ȸ�� ��忡��  rootNode �� true�� ��带 ��ȯ.
        //MindMapNodeInfo root = MindMapManager.instance.RootFindTree();
        //print("���� ��ȯ�� ��Ʈ ��� ������ : " + root.DATA);


        // 2. ���ε� ���� root�� �ڽ� ����Ʈ�� ��� 
        //MindMapManager.instance.PrintChildTree(root); 

        // 3. ���ε� �� ��ü ����ϴ� �޼ҵ� 
        //MindMapManager.instance.RootPrintTree(root); 


        // �߰� ��带 �����ϴ� ��� ���� 
        // 1. ���� ������ ���ε� �ʿ��� ���õ� ��� MindMapController UpdateSeleted() �޼ҵ带 ���� ����� ������ �����´�.
        // 2. MindMapController UpdateDelete()���� �б⸦ ���� ��尡 ���� �������, �߰� ��������� Ȯ���Ѵ�. 
        // 3. �߰� ����� ���, FindSubTree(tempNode); ��� ���� Ʈ���� ��ȸ�ϸ� Destory�� �Ѵ�. 


    }


}
