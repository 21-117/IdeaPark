using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MindMapManager;

public class MindMapTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // MindMap 클래스의 MakeMindMapTree 메서드를 호출하여 마인드 맵을 만듭니다.
        MindMapNode<string> root = MindMap.MakeMindMapTree();

        // 만들어진 마인드 맵을 출력합니다.
        Debug.Log("마인드 맵: 출력");
        MindMap.PrintTree(root);

        // 마인드 맵의 높이를 가져와 출력합니다.
        int height = MindMap.GetHeight(root);
        Debug.Log("마인드 맵 높이: " + height);

    }

    
}
