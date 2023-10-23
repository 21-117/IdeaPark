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

    // ������ ���ε� ���� ������ �����ϴ� ����Ʈ
    public List<MindMapNodeInfo> nodeList;

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
        nodeList = new List<MindMapNodeInfo>(); 

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


    // ���ε� ���� �����Ͽ� ��ȯ�ϴ� �޼ҵ�. 
    public MindMapNodeInfo MakeMindMapTree()
    {
        // ���� ���ε�� ����.
        // �߽� ���� ��� : ���� ����
        // �ֿ� �б� ��� : ���Ƹ� Ȱ��, ���� �ð�, ���� Ȱ��
        // ���� �б� ���
        // 1. ���Ƹ� Ȱ�� -> { ���� ����, � ���Ƹ�, �ֿ� Ȱ��}
        // 2. ���� �ð� -> { ���� ����, ���� ���� ����, �н��� ����}
        // 3. ���� Ȱ�� -> { ���� ����, �˰Ե� ��, ������ ���}

        // ���� ����
        // �θ� - �ڽ� 
        // -> id�� �ο��ؾ��ؼ� count ���� �ø���.
        // 1. -> ù ������ ������ �θ� ���� id = 0�̴�. 
        // ����: id == 0 -> �߽� ���� ���� ����.
        // id != 0 ->  ������ ��忡�� ��� ���踦 ã�� ����. 


        // �����ϰ� ���ε� ��带 �����ϴ� �޼ҵ� ���� 
        // �ڽ��� �θ𺸴� id ���� Ŭ���� ����(����ó��)
        // 1. ����ڰ� ��带 �����Ѵ�. 
        // 2. ���ἱ�� ������ ���(�θ� ���)�� ����
        // 2-1. ������ �����ϴ� ��ũ��Ʈ�� ������ ������Ʈ�� �߰�.        
        // 2-2. ���� �� id ���� ���Ͽ� id ���� ū ��尡 �θ� - 
        // 3. ���� �� ������ ���(�θ� ���)�� ������ �����´�, 
        // 4. ������ ����� ������ �ڽ� ����Ʈ�� ������ �����Ѵ�. 


        MindMapNodeInfo root = new MindMapNodeInfo() { DATA = "���� ����" };
        {



            {
                MindMapNodeInfo node = new MindMapNodeInfo() { DATA = "���Ƹ� Ȱ��" };
                node.Children.Add(new MindMapNodeInfo() { DATA = "���� ����" });
                node.Children.Add(new MindMapNodeInfo() { DATA = "� ���Ƹ� : ���� -> ��" });
                node.Children.Add(new MindMapNodeInfo() { DATA = "�ֿ� Ȱ��" });
                root.Children.Add(node);
            }

            {
                MindMapNodeInfo node = new MindMapNodeInfo() { DATA = "���� �ð�" };
                node.Children.Add(new MindMapNodeInfo() { DATA = "���� ����" });
                node.Children.Add(new MindMapNodeInfo() { DATA = "���� ���� ����" });
                node.Children.Add(new MindMapNodeInfo() { DATA = "�н��� ����" });
                root.Children.Add(node);
            }

            {
                MindMapNodeInfo node = new MindMapNodeInfo() { DATA = "���� Ȱ��" };
                node.Children.Add(new MindMapNodeInfo() { DATA = "���� ����" });
                node.Children.Add(new MindMapNodeInfo() { DATA = "�˰Ե� ��" });
                node.Children.Add(new MindMapNodeInfo() { DATA = "������ ���" });
                root.Children.Add(node);
            }
        }

        return root;
    }

  

    // ���ε� ���� root�� �ڽ� ����Ʈ�� ��� 
    public void RootPrintTree(MindMapNodeInfo root)
    {
        foreach(var node in root.Children) {
            Debug.Log(node.DATA); 
        }
    }

    // ���ε� ���� ��� ��ü�� ����ϴ� �޼ҵ�. -> (��ü�� �����ϴ� �޼ҵ�� Ȱ��) 
    public  void PrintTree(MindMapNodeInfo root)
    {

        // ����� GameObject�� �����.
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // ������ ����, ��ġ, ȸ���� �����ϰ� ����.
        go.transform.position = Random.onUnitSphere * Random.Range(3.0f, 5.0f);
        go.transform.rotation = Random.rotation;

        // ���� ���ε�� ����� ������ �����ؼ� ��� 
        Debug.Log("���� ����� ID : " + root.ID + ",  ���� ����� DATA : " + root.DATA);

        // ��������� �ڽĵ��� ������ ����
        foreach (MindMapNodeInfo child in root.Children)
        {
            PrintTree(child);
        }

    }

    // ���ε� ���� �б⿡ ���� ���̸� ��ȯ�ϴ� �޼ҵ�. 
    public  int FindGetHeight(MindMapNodeInfo root)
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
    public MindMapNodeInfo FindAncestor(MindMapNodeInfo node)
    {
        return null; 
    }

    // �ش� ����� ���ε� �� ���� Ʈ���� ã�Ƽ� ��ȯ�ϴ� �޼ҵ� .   
    public List<MindMapNodeInfo> FindSubTree(MindMapNodeInfo node)
    {
        return null;     
    }

    // ������ nodeList �������� JSON ���·� �����ϴ� �޼ҵ� 
    public void SavedNodeList()
    {

    }

}