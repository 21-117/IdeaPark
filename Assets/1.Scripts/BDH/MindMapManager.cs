using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;

namespace MindMapManager
{
    // ���ε���� �����ϰ� �����ϴ� Ŭ����
    public class MindMapRoot
    {
        public MindMapNode Root { get; private set; }

        public MindMapRoot()
        {
            Root = new MindMapNode("Root");
        }

        // ���ο� ��带 �߰��ϴ� �޼���
        public MindMapNode AddNode(MindMapNode parent, string text)
        {
            MindMapNode newNode = new MindMapNode(text);
            parent.Children.Add(newNode);
            return newNode;
        }

        // ��带 �����ϴ� �޼���
        public void RemoveNode(MindMapNode parent, MindMapNode nodeToRemove)
        {
            parent.Children.Remove(nodeToRemove);
        }

        // ���ε���� �ؽ�Ʈ�� ����ϴ� �޼��� (��������� ȣ���)
        private void PrintNode(MindMapNode node, string indent = "")
        {
            Console.WriteLine(indent + node.Text);
            foreach (var child in node.Children)
            {
                PrintNode(child, indent + "  ");
            }
        }

        // ��ü ���ε���� ����ϴ� �޼���
        public void PrintMindMap()
        {
            PrintNode(Root);
        }

    }

    // ���ε���� �� ��带 ��Ÿ���� Ŭ����
    public class MindMapNode
    {
        
        private string text;
        private Image image;

        public string Text { get; set; }
        public List<MindMapNode> Children { get; set; }

        public MindMapNode(string text)
        {
            Text = text;
            Children = new List<MindMapNode>();
        }
    }

}