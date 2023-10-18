using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MindMapManager
{
    // ���ε���� �����ϴ� ��� 
    class MindMapNode<T>
    {
        private T data { get; set; }  // �ؽ�Ʈ ����. 
        private GameObject nodeObject; // ���� ������Ʈ. 
        private Color color; // �� ����.
        private T aiData; // AI Ű���� ��õ ����. 
        public bool isSelected; // ���� ����.

        // �ڽ� ���ε�� ��忡 ���� ������ ����Ʈ ���� ����. 
        public List<MindMapNode<T>> Children { get; set; } = new List<MindMapNode<T>>();

        // �ؽ�Ʈ ������ ���� ���� ������Ƽ
        public T DATA
        {
            get { return data; }
            set { data = value; }
        }

        // ���� ������Ʈ�� ���� ���� ������Ƽ
        public GameObject NODEOBJECT
        {
            get { return nodeObject; }
            set { nodeObject = value; } 
        }

        // ���� ���� ���� ������Ƽ
        public Color COLOR
        {
            get { return color; }
            set { color = value; }  
        }

        // AI Ű���� ��õ ������ ���� ���� ������Ƽ
        public T AIDATA 
        {
            get { return aiData; }
            set { aiData = value; } 
        }

        
    }
    
    class MindMap
    {

        // ���ε� ���� �����Ͽ� ��ȯ�ϴ� �޼ҵ�. 
        public static MindMapNode<string> MakeMindMapTree()
        {
            // ���� ���ε�� ����.
            // �߽� ���� ��� : ���� ����
            // �ֿ� �б� ��� : ���Ƹ� Ȱ��, ���� �ð�, ���� Ȱ��
            // ���� �б� ���
            // 1. ���Ƹ� Ȱ�� -> { ���� ����, � ���Ƹ�, �ֿ� Ȱ��}
            // 2. ���� �ð� -> { ���� ����, ���� ���� ����, �н��� ����}
            // 3. ���� Ȱ�� -> { ���� ����, �˰Ե� ��, ������ ���}

            MindMapNode<string> root = new MindMapNode<string>() { DATA = "���� ����" };
            {
                {
                    MindMapNode<string> node = new MindMapNode<string>() { DATA = "���Ƹ� Ȱ��" };
                    node.Children.Add(new MindMapNode<string>() { DATA = "���� ����" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "� ���Ƹ�" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "�ֿ� Ȱ��" });
                    root.Children.Add(node);
                }

                {
                    MindMapNode<string> node = new MindMapNode<string>() { DATA = "���� �ð�" };
                    node.Children.Add(new MindMapNode<string>() { DATA = "���� ����" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "���� ���� ����" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "�н��� ����" });
                    root.Children.Add(node);
                }

                {
                    MindMapNode<string> node = new MindMapNode<string>() { DATA = "���� Ȱ��" };
                    node.Children.Add(new MindMapNode<string>() { DATA = "���� ����" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "�˰Ե� ��" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "������ ���" });
                    root.Children.Add(node);
                }
            }

            return root;
        }

        // ���ε� ���� ��ü�� ����ϴ� �޼ҵ�. 
        public static void PrintTree(MindMapNode<string> root)
        {
            // ���� ���ε�� ����� ������ �����ؼ� ��� 
            Debug.Log(root.DATA);

            // ����� GameObject�� �����.
            root.NODEOBJECT = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // ������ ����, ��ġ, ȸ���� �����ϰ� ����.
            root.NODEOBJECT.transform.position = Random.onUnitSphere * Random.Range(3.0f, 5.0f); 
            root.NODEOBJECT.transform.rotation = Random.rotation; 

            // ��������� �ڽĵ��� ������ ����
            foreach (MindMapNode<string> child in root.Children)
            {
                PrintTree(child);
            }

        }

        // ���ε� ���� �б⿡ ���� ���̸� ��ȯ�ϴ� �޼ҵ�. 
        public static int GetHeight(MindMapNode<string> root)
        {
            int height = 0;

            foreach (MindMapNode<string> child in root.Children)
            {
                int newHeight = GetHeight(child) + 1;
                if (height < newHeight)
                    height = newHeight;
                // height = Math.Max(height, newHeight); �� ����
            }
            return height;
        }


        
    }

}