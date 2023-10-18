using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Datums;
using UnityEngine;

namespace MindMapManager
{
    // 마인드맵을 구성하는 노드를 저장하는 형태. 
    [System.Serializable]
    class SaveMinMapNode
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
    class MindMapNode
    {
        private string data;  // 텍스트 정보. 
        private int type; // 노드의 생성 종류 타입 [Cube, Sphere, Capsule, Cylinder] 
        private Transform tr; // 노드의 생성 위치 
        private Color color; // 색 정보.
        private string aiData; // AI 키워드 추천 정보. 
        public bool isSelected; // 선택 여부.

        // 자식 마인드맵 노드에 대한 정보를 리스트 형식 저장. 
        public List<MindMapNode> Children = new List<MindMapNode>();

        // 텍스트 정보에 대한 접근 프로퍼티
        public string DATA
        {
            get { return data; }
            set { data = value; }
        }

        // 게임 오브젝트에 대한 접근 프로퍼티
        public GameObject NODEOBJECT
        {
            get { return nodeObject; }
            set { nodeObject = value; } 
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
    
    class MindMap
    {

        // 마인드 맵을 생성하여 반환하는 메소드. 
        public static MindMapNode MakeMindMapTree()
        {
            // 예시 마인드맵 구조.
            // 중심 주제 노드 : 과학 실험
            // 주요 분기 노드 : 동아리 활동, 수업 시간, 독서 활동
            // 하위 분기 노드
            // 1. 동아리 활동 -> { 가입 이유, 어떤 동아리, 주요 활동}
            // 2. 수업 시간 -> { 관련 과목, 과목 선택 이유, 학습한 내용}
            // 3. 독서 활동 -> { 관련 독서, 알게된 점, 독서의 계기}

            MindMapNode root = new MindMapNode() { DATA = "과학 실험" };
            {
                {
                    MindMapNode node = new MindMapNode() { DATA = "동아리 활동" };
                    node.Children.Add(new MindMapNode() { DATA = "가입 이유" });
                    node.Children.Add(new MindMapNode() { DATA = "어떤 동아리" });
                    node.Children.Add(new MindMapNode() { DATA = "주요 활동" });
                    root.Children.Add(node);
                }

                {
                    MindMapNode node = new MindMapNode() { DATA = "수업 시간" };
                    node.Children.Add(new MindMapNode() { DATA = "관련 과목" });
                    node.Children.Add(new MindMapNode() { DATA = "과목 선택 이유" });
                    node.Children.Add(new MindMapNode() { DATA = "학습한 내용" });
                    root.Children.Add(node);
                }

                {
                    MindMapNode node = new MindMapNode() { DATA = "독서 활동" };
                    node.Children.Add(new MindMapNode() { DATA = "관련 독서" });
                    node.Children.Add(new MindMapNode() { DATA = "알게된 점" });
                    node.Children.Add(new MindMapNode() { DATA = "독서의 계기" });
                    root.Children.Add(node);
                }
            }

            return root;
        }

        // 마인드 맵을 전체를 출력하는 메소드. 
        public static void PrintTree(MindMapNode root)
        {
            // 현재 마인드맵 노드의 데이터 접근해서 출력 
            Debug.Log(root.DATA);

            // 노드의 GameObject를 만든다.
            root.NODEOBJECT = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // 노드들의 색상, 위치, 회전은 랜덤하게 설정.
            root.NODEOBJECT.transform.position = Random.onUnitSphere * Random.Range(3.0f, 5.0f); 
            root.NODEOBJECT.transform.rotation = Random.rotation; 

            // 재귀적으로 자식들의 데이터 접근
            foreach (MindMapNode child in root.Children)
            {
                PrintTree(child);
            }

        }

        // 마인드 맵의 분기에 대한 깊이를 반환하는 메소드. 
        public static int GetHeight(MindMapNode root)
        {
            int height = 0;

            foreach (MindMapNode child in root.Children)
            {
                int newHeight = GetHeight(child) + 1;
                if (height < newHeight)
                    height = newHeight;
                // height = Math.Max(height, newHeight); 와 같음
            }
            return height;
        }


        
    }

}