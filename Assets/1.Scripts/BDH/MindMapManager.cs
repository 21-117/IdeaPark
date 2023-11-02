using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;



// ���ε���� �����ϴ� ��带 �����ϴ� ����. 
[System.Serializable]
public class SaveMinMapNode
{
    public string data; // �ؽ�Ʈ ����
    public int type; // ��� ���� ����
    public Vector3 pos; // ����� ��ġ 
    public Quaternion rot; // ����� ȸ�� 
    public Vector3 scale;  // ����� ũ�� 
    public Vector3 color; // ����� ����
    public string aiData; // AI �ؽ�Ʈ ������ 
    public bool isSelected; // ����� ���� ���� 
    public List<MindMapNodeInfo> Children; // �ڽ� ��� ����Ʈ 
}

public class MindMapManager : MonoBehaviour
{
    // ���ε�� �̱��� �������� ����.
    // static ������ ���� �ش� �ν��Ͻ��� �ܺο��� ���� �����ϰ� ��.
    public static MindMapManager instance;

    // ���ε� ���� ��Ʈ ��� 
    private MindMapNodeInfo rootNode;

    // ������ ���ε� ���� ������ ����Ǿ� �ִ� ���� ������Ʈ.
    public GameObject nodeManager;

    // ��Ʈ ��忡 ���� ������Ƽ 
    public MindMapNodeInfo ROOTNODE
    {
        get { return rootNode; }
        set { rootNode = value; }
    }

    // ��庰 ID �� �ο�
    private static int id = 0;

    public int GetID
    {
        get { return id; }
        set { id = value; }
    }

    private void Awake()
    {

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


    //���ε� ���� �����Ͽ� ��ȯ�ϴ� �޼ҵ�.
    //public MindMapNodeInfo MakeMindMapTree()
    //{
    //    ���� ���ε�� ����.
    //     �߽� ���� ���: ���� ����
    //     �ֿ� �б� ���: ���Ƹ� Ȱ��, ���� �ð�, ���� Ȱ��
    //     ���� �б� ���
    //     1.���Ƹ� Ȱ��-> { ���� ����, � ���Ƹ�, �ֿ� Ȱ��}
    //    2.���� �ð�-> { ���� ����, ���� ���� ����, �н��� ����}
    //    3.���� Ȱ��-> { ���� ����, �˰Ե� ��, ������ ���}

    //    ���� ����
    //     �θ� - �ڽ�
    //     ->id�� �ο��ؾ��ؼ� count ���� �ø���.
    //     1. ->ù ������ ������ �θ� ���� id = 0�̴�.
    //     ����: id == 0->�߽� ���� ���� ����.
    //     id != 0->������ ��忡�� ��� ���踦 ã�� ����.


    //     �����ϰ� ���ε� ��带 �����ϴ� �޼ҵ� ����
    //     �ڽ��� �θ𺸴� id ���� Ŭ���� ����(����ó��)
    //     1.����ڰ� ��带 �����Ѵ�. 
    //     2.���ἱ�� ������ ���(�θ� ���)�� ����
    //     2 - 1.������ �����ϴ� ��ũ��Ʈ�� ������ ������Ʈ�� �߰�.
    //     2 - 2.���� �� id ���� ���Ͽ� id ���� ū ��尡 �θ� -
    //     3.���� �� ������ ���(�θ� ���)�� ������ �����´�, 
    //     4.������ ����� ������ �ڽ� ����Ʈ�� ������ �����Ѵ�. 


    //    MindMapNodeInfo root = new MindMapNodeInfo() { DATA = "���� ����" };
    //    {


    //        {
    //            1.�ڽ� ��� ����
    //            MindMapNodeInfo node = new MindMapNodeInfo() { DATA = "���Ƹ� Ȱ��" };
    //            2.�θ�, �ڽ� ��� �Ǵ� ���� ����(�ڽ� ����Ʈ�� �߰�)
    //            root.Children.Add(node);

    //            1, 2�� ������ 3�� �ݺ��Ѵ�. 
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "���� ����" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "� ���Ƹ� : ���� -> ��" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "�ֿ� Ȱ��" });

    //        }

    //        {
    //            MindMapNodeInfo node = new MindMapNodeInfo() { DATA = "���� �ð�" };
    //            root.Children.Add(node);
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "���� ����" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "���� ���� ����" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "�н��� ����" });

    //        }

    //        {
    //            MindMapNodeInfo node = new MindMapNodeInfo() { DATA = "���� Ȱ��" };
    //            root.Children.Add(node);
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "���� ����" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "�˰Ե� ��" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "������ ���" });

    //        }
    //    }

    //    return root;
    //}

    // ���ε� ���� ROOT ��带 ã�Ƽ� ��ȯ�ϴ� �޼ҵ� 
    public MindMapNodeInfo RootFindTree()
    {
        // MindNodeManager ������Ʈ�� �ڽ� ������ ��ȸ�Ѵ�.
        for (int i = 0; i < nodeManager.transform.childCount; i++)
        {
            if (nodeManager.transform.GetChild(i).GetComponentInChildren<MindMapNodeInfo>().ROOTNODE == true)
            {
                // �ڽ� ����� Getcomponenent ��ũ��Ʈ�� MindMapNodeInfo���� true�� ��带 ã�� ��ȯ�Ѵ�. 
                return nodeManager.transform.GetChild(i).GetComponentInChildren<MindMapNodeInfo>();
            }
        }

        return null;
    }

    // ���ε� ���� root�� �ڽ� ����Ʈ�� ��� 
    public void PrintChildListNode(MindMapNodeInfo root)
    {
        foreach (var node in root.Children)
        {
            Debug.Log(node.DATA);
        }
    }

    // ���ε� ���� root�� �ڽ� ����Ʈ 1�� ��ȯ.
    public MindMapNodeInfo returnRootNode(MindMapNodeInfo root, string name)
    {
        for (int i = 0; i < root.Children.Count; i++)
        {
            if (root.Children[i].DATA == name)
            {
                print("ã�� ������ ��� : " + root.Children[i].DATA);
                return root.Children[i];
            }
        }

        return null;
    }

    // ���ε� ���� ��� ��ü�� ����ϴ� �޼ҵ�. -> (��ü�� �����ϴ� �޼ҵ�� Ȱ��)
    public void PrintTree(MindMapNodeInfo root)
    {

        // ���� ���ε�� ����� ������ �����ؼ� ��� 
        if (root.ROOTNODE)
        {
            // ��Ʈ ��� ���
            print("��Ʈ ����� ID : " + root.ID + ",  ���� ����� DATA : " + root.DATA);
        }
        else
        {
            // �ڽ� ��� ��� 
            print("�ڽ� ����� ID : " + root.ID + ",  ���� ����� DATA : " + root.DATA);
        }

        // ��������� �ڽĵ��� ������ ����
        foreach (MindMapNodeInfo child in root.Children)
        {
            PrintTree(child);
        }

    }

    // ���ε� ���� �б⿡ ���� ���̸� ��ȯ�ϴ� �޼ҵ�. 
    // ���� ó�� - > ���̰� ���� ���鳢���� ������ �� ������ ����. 
    public int FindGetHeight(MindMapNodeInfo root)
    {
        int height = 0;

        foreach (MindMapNodeInfo child in root.Children)
        {
            int newHeight = FindGetHeight(child) + 1;
            if (height < newHeight)
                height = newHeight;
            // height = Math.Max(height, newHeight); �� ����
        }
        return height;
    }

    // ���� ���ε� ���� ��忡�� ��� ���� ��带 ã�� ��ȯ�ϴ� �޼ҵ�.
    public MindMapNodeInfo FindAncestor(MindMapNodeInfo currentNode)
    {

        return null;
    }

    // �ش� ����� ���ε� �� ���� Ʈ���� ã�Ƽ� ��ȯ�ϴ� �޼ҵ� . ( ��ȯŸ�� : List<MindMapNodeInfo>)  
    public void FindSubTree(MindMapNodeInfo currentNode)
    {

        // ���� ���ε�� ����� ������ �����ؼ� ��� 
        print("���� ����� ID : " + currentNode.ID + ",  ���� ����� DATA : " + currentNode.DATA);

        // ��������� �ڽ��� ����Ʈ�� ������ ����
        foreach (MindMapNodeInfo child in currentNode.Children)
        {
            FindSubTree(child);
        }


    }

    public void DeleteSubTree(MindMapNodeInfo root)
    {

        print("���� ������ ����Ʈ�� ��� : " + root.transform.parent.gameObject);
        Destroy(root.transform.parent.gameObject);
        // ��������� �ڽĵ��� ������ ����
        foreach (MindMapNodeInfo child in root.Children)
        {
            DeleteSubTree(child);
        }

    }

    // ������ nodeList �������� JSON ���·� �����ϴ� �޼ҵ� 
    public void SavedNodeList()
    {

    }

}