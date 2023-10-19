using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeInfo;
using UnityEngine.XR.OpenXR.Input;

public class MindMap : MonoBehaviour
{
    // 마인드맵 싱글톤 패턴으로 적용.
    // static 변수를 통해 해당 인스턴스를 외부에서 접근 가능하게 함.
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

    // 노드별 ID 값 부여
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


    // 마인드 맵을 생성하여 반환하는 메소드. 
    public  MindMapNode MakeMindMapTree()
    {
        // 예시 마인드맵 구조.
        // 중심 주제 노드 : 과학 실험
        // 주요 분기 노드 : 동아리 활동, 수업 시간, 독서 활동
        // 하위 분기 노드
        // 1. 동아리 활동 -> { 가입 이유, 어떤 동아리, 주요 활동}
        // 2. 수업 시간 -> { 관련 과목, 과목 선택 이유, 학습한 내용}
        // 3. 독서 활동 -> { 관련 독서, 알게된 점, 독서의 계기}

        // 구현 로직
        // 부모 - 자식 
        // -> id를 부여해야해서 count 값을 올린다.
        // 1. -> 첫 번쨰로 생성된 부모 노드는 id = 0이다. 
        // 로직: id == 0 -> 중심 주제 노드로 설정.
        // id != 0 ->  나머지 노드에서 상관 관계를 찾아 연결. 
      

        // 삽입하고 마인드 노드를 생성하는 메소드 구현 
        // 자식은 부모보다 id 값이 클수가 없다(예외처리)
        // 1. 사용자가 노드를 생성한다. 
        // 2. 연결선을 연결할 노드(부모 노드)에 연결
        // 2-1. 라인을 생성하는 스크립트를 생성할 오브젝트에 추가.        
        // 2-2. 연결 시 id 값을 비교하여 id 값이 큰 노드가 부모 - 
        // 3. 연결 시 연결할 노드(부모 노드)의 정보를 가져온다, 
        // 4. 가져온 노드의 정보의 자식 리스트에 생성된 삽입한다. 
        

        MindMapNode root = new MindMapNode() { DATA = "과학 실험" };
        {



            {
                MindMapNode node = new MindMapNode() { DATA = "동아리 활동" };
                node.Children.Add(new MindMapNode() { DATA = "가입 이유" });
                node.Children.Add(new MindMapNode() { DATA = "어떤 동아리 : 목적 -> 술" });
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

    // 마인드 맵을 전체를 LineRenderer에서 연결하는 메소드.
    public void ConnectionNode()
    {

    }

    // 마인드 맵의 root의 자식 리스트를 출력 
    public void RootPrintTree(MindMapNode root)
    {
        foreach(var node in root.Children) {
            Debug.Log(node.DATA); 
        }
    }

    // 마인드 맵을 전체를 출력하는 메소드. 
    public  void PrintTree(MindMapNode root)
    {

        // 노드의 GameObject를 만든다.
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // 노드들의 색상, 위치, 회전은 랜덤하게 설정.
        go.transform.position = Random.onUnitSphere * Random.Range(3.0f, 5.0f);
        go.transform.rotation = Random.rotation;

        // 현재 마인드맵 노드의 데이터 접근해서 출력 
        // Debug.Log("현재 노드의 ID : " + root.ID + ",  현재 노드의 DATA : " + root.DATA);

        // 재귀적으로 자식들의 데이터 접근
        foreach (MindMapNode child in root.Children)
        {
            PrintTree(child);
        }

    }

    // 마인드 맵의 분기에 대한 깊이를 반환하는 메소드. 
    public  int GetHeight(MindMapNode root)
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

    // 현재 마인드 맵의 노드에서 모든 조상 노드를 찾아 반환하는 메소드.
    


}