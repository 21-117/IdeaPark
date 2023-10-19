
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 데이터 생성 및 저장하는 템플릿 
namespace NodeInfo
{

    // 마인드맵을 구성하는 노드를 저장하는 형태. 
    [System.Serializable]
    public class SaveMinMapNode
    {
        public string data; // 텍스트 정보
        public int type; // 노드 생성 종류
        public Vector3 pos; // 노드의 위치 
        public Quaternion rot; // 노드의 회전 
        public Vector3 scale;  // 노드의 크기 
        public Vector3 color; // 노드의 색상
        public string aiData; // AI 텍스트 데이터 
        public bool isSelected; // 사용자 선택 여부 
        public List<MindMapNode> Children; // 자식 노드 리스트 
    }

    // 마인드맵을 구성하는 노드 
    [System.Serializable]
    public class MindMapNode
    {
        private int id; // 노드의 ID 
        private string data;  // 텍스트 정보. 
        private int type; // 노드의 생성 종류 타입 [Cube, Sphere, Capsule, Cylinder] 
        private Transform tr; // 노드의 생성 위치 
        private Color color; // 색 정보.
        private string aiData; // AI 키워드 추천 정보. 
        public bool isSelected; // 선택 여부.

        // 자식 마인드맵 노드에 대한 정보를 리스트 형식 저장. 
        public List<MindMapNode> Children = new List<MindMapNode>();

        public MindMapNode()
        {
            this.id = MindMap.instance.GetID;
            // 각 노드의 ID 값을 출력
        
            // 노드가 생성될 때 마다 ID 값이 1씩 증가한다.
            MindMap.instance.GetID += 1; 
        }

        // ID 정보에 대한 접근 프로퍼티
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        // 텍스트 정보에 대한 접근 프로퍼티
        public string DATA
        {
            get { return data; }
            set { data = value; }
        }

        // 노드의 생성 위치에 대한 접근 프로퍼티
        public Transform POSITION
        {
            get { return tr; }
            set { tr = value; }
        }

        // 색에 대한 접근 프로퍼티
        public Color COLOR
        {
            get { return color; }
            set { color = value; }
        }

        // AI 키워드 추천 정보에 대한 접근 프로퍼티
        public string AIDATA
        {
            get { return aiData; }
            set { aiData = value; }
        }


    }


}