using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeInfo;

public class MindMapTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // MindMap 클래스의 MakeMindMapTree 메서드를 호출하여 마인드 맵을 만듭니다.
        MindMapNode root = MindMap.Get().MakeMindMapTree();

        // Root 노드 전체 DATA 값 출력 
        MindMap.instance.RootPrintTree(root);

        // 각 노드의 전체 ID 값을 출력
       //Debug.Log("마인드 맵 전체 ID: 출력" + MindMap.instance.GetID);

        // 만들어진 마인드 맵을 출력합니다.
        //Debug.Log("마인드 맵: 출력");
        MindMap.instance.PrintTree(root);

        // 마인드 맵의 높이를 가져와 출력합니다.
        int height = MindMap.instance.GetHeight(root);
        //Debug.Log("마인드 맵 높이: " + height);

    }

    
}
