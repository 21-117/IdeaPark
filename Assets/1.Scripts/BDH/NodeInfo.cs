
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ ���� �� �����ϴ� ���ø� 
namespace NodeInfo
{

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
        public List<MindMapNode> Children; // �ڽ� ��� ����Ʈ 
    }

    // ���ε���� �����ϴ� ��� 
    [System.Serializable]
    public class MindMapNode
    {
        private int id; // ����� ID 
        private string data;  // �ؽ�Ʈ ����. 
        private int type; // ����� ���� ���� Ÿ�� [Cube, Sphere, Capsule, Cylinder] 
        private Transform tr; // ����� ���� ��ġ 
        private Color color; // �� ����.
        private string aiData; // AI Ű���� ��õ ����. 
        public bool isSelected; // ���� ����.

        // �ڽ� ���ε�� ��忡 ���� ������ ����Ʈ ���� ����. 
        public List<MindMapNode> Children = new List<MindMapNode>();

        public MindMapNode()
        {
            this.id = MindMap.instance.GetID;
            // �� ����� ID ���� ���
        
            // ��尡 ������ �� ���� ID ���� 1�� �����Ѵ�.
            MindMap.instance.GetID += 1; 
        }

        // ID ������ ���� ���� ������Ƽ
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        // �ؽ�Ʈ ������ ���� ���� ������Ƽ
        public string DATA
        {
            get { return data; }
            set { data = value; }
        }

        // ����� ���� ��ġ�� ���� ���� ������Ƽ
        public Transform POSITION
        {
            get { return tr; }
            set { tr = value; }
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


}