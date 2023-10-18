using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Datums;
using UnityEngine;

namespace MindMapManager
{
    // ���ε���� �����ϴ� ��带 �����ϴ� ����. 
    [System.Serializable]
    class SaveMinMapNode
    {
        public string data; // �ؽ�Ʈ ����
        public int type; // ��� ���� ����
        public Vector3 pos; // ����� ��ġ 
        public Quaternion rot; // ����� ȸ�� 
        public Vector3 scale;  // ����� ũ�� 
        public Vector3 color; // ����� ����
        public string aiData; // AI �ؽ�Ʈ ������ 
        public bool isSelected; // ����� ���� ���� 
        public List<MindMapNode> Children; // �ڽ� ��� ����Ʈ 
    }

    // ���ε���� �����ϴ� ��� 
    [System.Serializable]
    class MindMapNode
    {
        private string data;  // �ؽ�Ʈ ����. 
        private int type; // ����� ���� ���� Ÿ�� [Cube, Sphere, Capsule, Cylinder] 
        private Transform tr; // ����� ���� ��ġ 
        private Color color; // �� ����.
        private string aiData; // AI Ű���� ��õ ����. 
        public bool isSelected; // ���� ����.

        // �ڽ� ���ε�� ��忡 ���� ������ ����Ʈ ���� ����. 
        public List<MindMapNode> Children = new List<MindMapNode>();

        // �ؽ�Ʈ ������ ���� ���� ������Ƽ
        public string DATA
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
        public string AIDATA 
        {
            get { return aiData; }
            set { aiData = value; } 
        }

        
    }
    
    class MindMap
    {

        // ���ε� ���� �����Ͽ� ��ȯ�ϴ� �޼ҵ�. 
        public static MindMapNode MakeMindMapTree()
        {
            // ���� ���ε�� ����.
            // �߽� ���� ��� : ���� ����
            // �ֿ� �б� ��� : ���Ƹ� Ȱ��, ���� �ð�, ���� Ȱ��
            // ���� �б� ���
            // 1. ���Ƹ� Ȱ�� -> { ���� ����, � ���Ƹ�, �ֿ� Ȱ��}
            // 2. ���� �ð� -> { ���� ����, ���� ���� ����, �н��� ����}
            // 3. ���� Ȱ�� -> { ���� ����, �˰Ե� ��, ������ ���}

            MindMapNode root = new MindMapNode() { DATA = "���� ����" };
            {
                {
                    MindMapNode node = new MindMapNode() { DATA = "���Ƹ� Ȱ��" };
                    node.Children.Add(new MindMapNode() { DATA = "���� ����" });
                    node.Children.Add(new MindMapNode() { DATA = "� ���Ƹ�" });
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

        // ���ε� ���� ��ü�� ����ϴ� �޼ҵ�. 
        public static void PrintTree(MindMapNode root)
        {
            // ���� ���ε�� ����� ������ �����ؼ� ��� 
            Debug.Log(root.DATA);

            // ����� GameObject�� �����.
            root.NODEOBJECT = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // ������ ����, ��ġ, ȸ���� �����ϰ� ����.
            root.NODEOBJECT.transform.position = Random.onUnitSphere * Random.Range(3.0f, 5.0f); 
            root.NODEOBJECT.transform.rotation = Random.rotation; 

            // ��������� �ڽĵ��� ������ ����
            foreach (MindMapNode child in root.Children)
            {
                PrintTree(child);
            }

        }

        // ���ε� ���� �б⿡ ���� ���̸� ��ȯ�ϴ� �޼ҵ�. 
        public static int GetHeight(MindMapNode root)
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


        
    }

}