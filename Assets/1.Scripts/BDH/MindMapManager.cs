using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MindMapManager
{
    // 마인드맵을 구성하는 노드 
    class MindMapNode<T>
    {
        private T data { get; set; }  // 텍스트 정보. 
        private GameObject nodeObject; // 게임 오브젝트. 
        private Color color; // 색 정보.
        private T aiData; // AI 키워드 추천 정보. 
        public bool isSelected; // 선택 여부.

        // 자식 마인드맵 노드에 대한 정보를 리스트 형식 저장. 
        public List<MindMapNode<T>> Children { get; set; } = new List<MindMapNode<T>>();

        // 텍스트 정보에 대한 접근 프로퍼티
        public T DATA
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
        public T AIDATA 
        {
            get { return aiData; }
            set { aiData = value; } 
        }

        
    }
    
    class MindMap
    {

        // 마인드 맵을 생성하여 반환하는 메소드. 
        public static MindMapNode<string> MakeMindMapTree()
        {
            // 예시 마인드맵 구조.
            // 중심 주제 노드 : 과학 실험
            // 주요 분기 노드 : 동아리 활동, 수업 시간, 독서 활동
            // 하위 분기 노드
            // 1. 동아리 활동 -> { 가입 이유, 어떤 동아리, 주요 활동}
            // 2. 수업 시간 -> { 관련 과목, 과목 선택 이유, 학습한 내용}
            // 3. 독서 활동 -> { 관련 독서, 알게된 점, 독서의 계기}

            MindMapNode<string> root = new MindMapNode<string>() { DATA = "과학 실험" };
            {
                {
                    MindMapNode<string> node = new MindMapNode<string>() { DATA = "동아리 활동" };
                    node.Children.Add(new MindMapNode<string>() { DATA = "가입 이유" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "어떤 동아리" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "주요 활동" });
                    root.Children.Add(node);
                }

                {
                    MindMapNode<string> node = new MindMapNode<string>() { DATA = "수업 시간" };
                    node.Children.Add(new MindMapNode<string>() { DATA = "관련 과목" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "과목 선택 이유" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "학습한 내용" });
                    root.Children.Add(node);
                }

                {
                    MindMapNode<string> node = new MindMapNode<string>() { DATA = "독서 활동" };
                    node.Children.Add(new MindMapNode<string>() { DATA = "관련 독서" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "알게된 점" });
                    node.Children.Add(new MindMapNode<string>() { DATA = "독서의 계기" });
                    root.Children.Add(node);
                }
            }

            return root;
        }

        // 마인드 맵을 전체를 출력하는 메소드. 
        public static void PrintTree(MindMapNode<string> root)
        {
            // 현재 마인드맵 노드의 데이터 접근해서 출력 
            Debug.Log(root.DATA);

            // 노드의 GameObject를 만든다.
            root.NODEOBJECT = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // 노드들의 색상, 위치, 회전은 랜덤하게 설정.
            root.NODEOBJECT.transform.position = Random.onUnitSphere * Random.Range(3.0f, 5.0f); 
            root.NODEOBJECT.transform.rotation = Random.rotation; 

            // 재귀적으로 자식들의 데이터 접근
            foreach (MindMapNode<string> child in root.Children)
            {
                PrintTree(child);
            }

        }

        // 마인드 맵의 분기에 대한 깊이를 반환하는 메소드. 
        public static int GetHeight(MindMapNode<string> root)
        {
            int height = 0;

            foreach (MindMapNode<string> child in root.Children)
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