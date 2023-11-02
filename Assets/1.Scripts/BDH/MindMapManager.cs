using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;



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
    public List<MindMapNodeInfo> Children; // 자식 노드 리스트 
}

public class MindMapManager : MonoBehaviour
{
    // 마인드맵 싱글톤 패턴으로 적용.
    // static 변수를 통해 해당 인스턴스를 외부에서 접근 가능하게 함.
    public static MindMapManager instance;

    // 마인드 맵의 루트 노드 
    private MindMapNodeInfo rootNode;

    // 생성된 마인드 맵의 노드들을 저장되어 있는 게임 오브젝트.
    public GameObject nodeManager;

    // 루트 노드에 대한 프로퍼티 
    public MindMapNodeInfo ROOTNODE
    {
        get { return rootNode; }
        set { rootNode = value; }
    }

    // 노드별 ID 값 부여
    private static int id = 0;

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


    //마인드 맵을 생성하여 반환하는 메소드.
    //public MindMapNodeInfo MakeMindMapTree()
    //{
    //    예시 마인드맵 구조.
    //     중심 주제 노드: 과학 실험
    //     주요 분기 노드: 동아리 활동, 수업 시간, 독서 활동
    //     하위 분기 노드
    //     1.동아리 활동-> { 가입 이유, 어떤 동아리, 주요 활동}
    //    2.수업 시간-> { 관련 과목, 과목 선택 이유, 학습한 내용}
    //    3.독서 활동-> { 관련 독서, 알게된 점, 독서의 계기}

    //    구현 로직
    //     부모 - 자식
    //     ->id를 부여해야해서 count 값을 올린다.
    //     1. ->첫 번쨰로 생성된 부모 노드는 id = 0이다.
    //     로직: id == 0->중심 주제 노드로 설정.
    //     id != 0->나머지 노드에서 상관 관계를 찾아 연결.


    //     삽입하고 마인드 노드를 생성하는 메소드 구현
    //     자식은 부모보다 id 값이 클수가 없다(예외처리)
    //     1.사용자가 노드를 생성한다. 
    //     2.연결선을 연결할 노드(부모 노드)에 연결
    //     2 - 1.라인을 생성하는 스크립트를 생성할 오브젝트에 추가.
    //     2 - 2.연결 시 id 값을 비교하여 id 값이 큰 노드가 부모 -
    //     3.연결 시 연결할 노드(부모 노드)의 정보를 가져온다, 
    //     4.가져온 노드의 정보의 자식 리스트에 생성된 삽입한다. 


    //    MindMapNodeInfo root = new MindMapNodeInfo() { DATA = "과학 실험" };
    //    {


    //        {
    //            1.자식 노드 생성
    //            MindMapNodeInfo node = new MindMapNodeInfo() { DATA = "동아리 활동" };
    //            2.부모, 자식 노드 판단 이후 연결(자식 리스트에 추가)
    //            root.Children.Add(node);

    //            1, 2번 과정을 3번 반복한다. 
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "가입 이유" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "어떤 동아리 : 목적 -> 술" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "주요 활동" });

    //        }

    //        {
    //            MindMapNodeInfo node = new MindMapNodeInfo() { DATA = "수업 시간" };
    //            root.Children.Add(node);
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "관련 과목" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "과목 선택 이유" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "학습한 내용" });

    //        }

    //        {
    //            MindMapNodeInfo node = new MindMapNodeInfo() { DATA = "독서 활동" };
    //            root.Children.Add(node);
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "관련 독서" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "알게된 점" });
    //            node.Children.Add(new MindMapNodeInfo() { DATA = "독서의 계기" });

    //        }
    //    }

    //    return root;
    //}

    // 마인드 맵의 ROOT 노드를 찾아서 반환하는 메소드 
    public MindMapNodeInfo RootFindTree()
    {
        // MindNodeManager 오브젝트의 자식 노드들을 순회한다.
        for (int i = 0; i < nodeManager.transform.childCount; i++)
        {
            if (nodeManager.transform.GetChild(i).GetComponentInChildren<MindMapNodeInfo>().ROOTNODE == true)
            {
                // 자식 노드의 Getcomponenent 스크립트인 MindMapNodeInfo에서 true인 노드를 찾아 반환한다. 
                return nodeManager.transform.GetChild(i).GetComponentInChildren<MindMapNodeInfo>();
            }
        }

        return null;
    }

    // 마인드 맵의 root의 자식 리스트를 출력 
    public void PrintChildListNode(MindMapNodeInfo root)
    {
        foreach (var node in root.Children)
        {
            Debug.Log(node.DATA);
        }
    }

    // 마인드 맵의 root의 자식 리스트 1번 반환.
    public MindMapNodeInfo returnRootNode(MindMapNodeInfo root, string name)
    {
        for (int i = 0; i < root.Children.Count; i++)
        {
            if (root.Children[i].DATA == name)
            {
                print("찾은 데이터 노드 : " + root.Children[i].DATA);
                return root.Children[i];
            }
        }

        return null;
    }

    // 마인드 맵의 노드 전체를 출력하는 메소드. -> (전체를 저장하는 메소드로 활용)
    public void PrintTree(MindMapNodeInfo root)
    {

        // 현재 마인드맵 노드의 데이터 접근해서 출력 
        if (root.ROOTNODE)
        {
            // 루트 노드 출력
            print("루트 노드의 ID : " + root.ID + ",  현재 노드의 DATA : " + root.DATA);
        }
        else
        {
            // 자식 노드 출력 
            print("자식 노드의 ID : " + root.ID + ",  현재 노드의 DATA : " + root.DATA);
        }

        // 재귀적으로 자식들의 데이터 접근
        foreach (MindMapNodeInfo child in root.Children)
        {
            PrintTree(child);
        }

    }

    // 마인드 맵의 분기에 대한 깊이를 반환하는 메소드. 
    // 예외 처리 - > 깊이가 같은 노드들끼리는 연결할 수 없도록 설정. 
    public int FindGetHeight(MindMapNodeInfo root)
    {
        int height = 0;

        foreach (MindMapNodeInfo child in root.Children)
        {
            int newHeight = FindGetHeight(child) + 1;
            if (height < newHeight)
                height = newHeight;
            // height = Math.Max(height, newHeight); 와 같음
        }
        return height;
    }

    // 현재 마인드 맵의 노드에서 모든 조상 노드를 찾아 반환하는 메소드.
    public MindMapNodeInfo FindAncestor(MindMapNodeInfo currentNode)
    {

        return null;
    }

    // 해당 노드의 마인드 맵 서브 트리를 찾아서 반환하는 메소드 . ( 반환타입 : List<MindMapNodeInfo>)  
    public void FindSubTree(MindMapNodeInfo currentNode)
    {

        // 현재 마인드맵 노드의 데이터 접근해서 출력 
        print("현재 노드의 ID : " + currentNode.ID + ",  현재 노드의 DATA : " + currentNode.DATA);

        // 재귀적으로 자식의 서브트리 데이터 접근
        foreach (MindMapNodeInfo child in currentNode.Children)
        {
            FindSubTree(child);
        }


    }

    public void DeleteSubTree(MindMapNodeInfo root)
    {

        print("현재 삭제할 서브트리 노드 : " + root.transform.parent.gameObject);
        Destroy(root.transform.parent.gameObject);
        // 재귀적으로 자식들의 데이터 접근
        foreach (MindMapNodeInfo child in root.Children)
        {
            DeleteSubTree(child);
        }

    }

    // 생성된 nodeList 정보들을 JSON 형태로 저장하는 메소드 
    public void SavedNodeList()
    {

    }

}