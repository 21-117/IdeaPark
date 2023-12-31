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

        // 마인드 맵 "수업 시간" 노드를 찾는다. 
        //MindMapNodeInfo tempNode = MindMapManager.instance.returnRootNode(root, "수업 시간");

        // 마인드 맵 "수업 시간" 모든 서브트리를 출력
        // MindMapManager.instance.FindSubTree(tempNode);

        // 마인드 맵의 root의 자식 리스트를 DATA 출력 
        //MindMapManager.instance.PrintChildTree(root);



        // 각 노드의 전체 ID 값을 출력
        //Debug.Log("마인드 맵 전체 ID: 출력" + MindMapManager.instance.GetID);

        // 만들어진 마인드 맵을 출력합니다.
        //Debug.Log("마인드 맵: 출력");
        //MindMapManager.instance.PrintTree(root);

        // 마인드 맵의 높이를 가져와 출력합니다.
        //int height = MindMap.instance.GetHeight(root);
        //Debug.Log("마인드 맵 높이: " + height);



        // 실전 테스트 해보자잇,,! 
        // ====================================================================

        // 1. 마인드 맵의 주제인 루트 노드를 찾는다. 
        // 루트 노드 반환 메소드 - > 마인드 맵 [ MindNodeManager ]을 순회한 노드에서  rootNode 가 true인 노드를 반환.
        //MindMapNodeInfo root = MindMapManager.instance.RootFindTree();
        //print("현재 반환된 루트 노드 데이터 : " + root.DATA);


        // 2. 마인드 맵의 root의 자식 리스트를 출력 
        //MindMapManager.instance.PrintChildTree(root); 

        // 3. 마인드 맵 전체 출력하는 메소드 
        //MindMapManager.instance.RootPrintTree(root); 


        // 중간 노드를 삭제하는 기능 구현 
        // 1. 현재 구성된 마인드 맵에서 선택된 노드 MindMapController UpdateSeleted() 메소드를 통해 노드의 정보를 가져온다.
        // 2. MindMapController UpdateDelete()에서 분기를 통해 노드가 리프 노드인지, 중간 노드인지를 확인한다. 
        // 3. 중간 노드인 경우, FindSubTree(tempNode); 모든 서브 트리를 순회하며 Destory를 한다. 


    }


}
