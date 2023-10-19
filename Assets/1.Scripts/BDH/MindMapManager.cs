using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeInfo;
using UnityEngine.XR.OpenXR.Input;

public class MindMap : MonoBehaviour
{
    // ���ε�� �̱��� �������� ����.
    // static ������ ���� �ش� �ν��Ͻ��� �ܺο��� ���� �����ϰ� ��.
    public static MindMap instance;
    
    public static MindMap Get()
    {
        if (instance == null)
        {
            GameObject mindMap = new GameObject("MindMapManager");
            mindMap.AddComponent<MindMap>();
        }

        return instance;
    }

    // ��庰 ID �� �ο�
    private static int id;

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


    // ���ε� ���� �����Ͽ� ��ȯ�ϴ� �޼ҵ�. 
    public  MindMapNode MakeMindMapTree()
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
        

        MindMapNode root = new MindMapNode() { DATA = "���� ����" };
        {



            {
                MindMapNode node = new MindMapNode() { DATA = "���Ƹ� Ȱ��" };
                node.Children.Add(new MindMapNode() { DATA = "���� ����" });
                node.Children.Add(new MindMapNode() { DATA = "� ���Ƹ� : ���� -> ��" });
                node.Children.Add(new MindMapNode() { DATA = "�ֿ� Ȱ��" });
                root.Children.Add(node);
            }

            {
                MindMapNode node = new MindMapNode() { DATA = "���� �ð�" };
                node.Children.Add(new MindMapNode() { DATA = "���� ����" });
                node.Children.Add(new MindMapNode() { DATA = "���� ���� ����" });
                node.Children.Add(new MindMapNode() { DATA = "�н��� ����" });
                root.Children.Add(node);
            }

            {
                MindMapNode node = new MindMapNode() { DATA = "���� Ȱ��" };
                node.Children.Add(new MindMapNode() { DATA = "���� ����" });
                node.Children.Add(new MindMapNode() { DATA = "�˰Ե� ��" });
                node.Children.Add(new MindMapNode() { DATA = "������ ���" });
                root.Children.Add(node);
            }
        }

        return root;
    }

    // ���ε� ���� ��ü�� LineRenderer���� �����ϴ� �޼ҵ�.
    public void ConnectionNode()
    {

    }

    // ���ε� ���� root�� �ڽ� ����Ʈ�� ��� 
    public void RootPrintTree(MindMapNode root)
    {
        foreach(var node in root.Children) {
            Debug.Log(node.DATA); 
        }
    }

    // ���ε� ���� ��ü�� ����ϴ� �޼ҵ�. 
    public  void PrintTree(MindMapNode root)
    {

        // ����� GameObject�� �����.
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // ������ ����, ��ġ, ȸ���� �����ϰ� ����.
        go.transform.position = Random.onUnitSphere * Random.Range(3.0f, 5.0f);
        go.transform.rotation = Random.rotation;

        // ���� ���ε�� ����� ������ �����ؼ� ��� 
        // Debug.Log("���� ����� ID : " + root.ID + ",  ���� ����� DATA : " + root.DATA);

        // ��������� �ڽĵ��� ������ ����
        foreach (MindMapNode child in root.Children)
        {
            PrintTree(child);
        }

    }

    // ���ε� ���� �б⿡ ���� ���̸� ��ȯ�ϴ� �޼ҵ�. 
    public  int GetHeight(MindMapNode root)
    {
        int height = 0;

        foreach (MindMapNode child in root.Children)
        {
            int newHeight = GetHeight(child) + 1;
            if (height < newHeight)
                height = newHeight;
            // height = Math.Max(height, newHeight); �� ����
        }
        return height;
    }

    // ���� ���ε� ���� ��忡�� ��� ���� ��带 ã�� ��ȯ�ϴ� �޼ҵ�.
    


}