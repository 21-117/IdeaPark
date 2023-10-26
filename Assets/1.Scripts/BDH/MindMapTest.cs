using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindMapTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // MindMap 클래스의 MakeMindMapTree 메서드를 호출하여 마인드 맵을 만듭니다.
        //MindMapNodeInfo root = MindMapManager.instance.MakeMindMapTree();

        // 마인드 맵의 root의 자식 리스트를 DATA 출력 
        //MindMapManager.instance.RootPrintTree(root);

        // 각 노드의 전체 ID 값을 출력
        //Debug.Log("마인드 맵 전체 ID: 출력" + MindMapManager.instance.GetID);

        // 만들어진 마인드 맵을 출력합니다.
        Debug.Log("마인드 맵: 출력");
        //MindMapManager.instance.PrintTree(root);

        // 마인드 맵의 높이를 가져와 출력합니다.
        //int height = MindMap.instance.GetHeight(root);
        //Debug.Log("마인드 맵 높이: " + height);



        // 실전 테스트 해보자잇,,! 
        // ====================================================================
        // 루트 노드 반환 메소드 - > 마인드 맵 [ MindNodeManager ]을 순회한 노드에서  rootNode 가 true인 노드를 반환.
        MindMapNodeInfo root = MindMapManager.instance.RootFindTree();
        print("현재 반환된 루트 노드 데이터 : " + root.DATA); 

    }


}
