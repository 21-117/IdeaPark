using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Datums;
using UnityEngine;
using NodeInfo; 

namespace MindMapManager
{
   
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

            // 부모 - 자식 
            // -> id를 부여해야해서 count 값을 올린다.

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
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // 노드들의 색상, 위치, 회전은 랜덤하게 설정.
            go.transform.position = Random.onUnitSphere * Random.Range(3.0f, 5.0f);
            go.transform.rotation = Random.rotation; 

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