using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Datums;
using UnityEngine;
using NodeInfo; 

namespace MindMapManager
{
   
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

            // �θ� - �ڽ� 
            // -> id�� �ο��ؾ��ؼ� count ���� �ø���.

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
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // ������ ����, ��ġ, ȸ���� �����ϰ� ����.
            go.transform.position = Random.onUnitSphere * Random.Range(3.0f, 5.0f);
            go.transform.rotation = Random.rotation; 

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