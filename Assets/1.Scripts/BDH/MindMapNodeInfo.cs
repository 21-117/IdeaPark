using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 노드 오브젝트의 전체적인 정보를 가지고 있는 클래스 
public class MindMapNodeInfo : MonoBehaviour 
{
    private bool rootNode; 
    private int id = 0; // 노드의 ID 
    private string data = "";  // 텍스트 정보. 
    private int type; // 노드의 생성 종류 타입 [Cube, Sphere, Capsule, Cylinder] 
    private Transform tr; // 노드의 생성 위치 
    private Color color; // 색 정보.
    private string aiData; // AI 키워드 추천 정보. 
    public bool isSelected; // 선택 여부.

    // 자식 마인드맵 노드에 대한 정보를 리스트 형식 저장. 
    public List<MindMapNodeInfo> Children = new List<MindMapNodeInfo>();

    public void UpdateNodeId()
    {
        this.id = MindMapManager.instance.GetID;
        // 각 노드의 ID 값을 출력

        // 노드가 생성될 때 마다 ID 값이 1씩 증가한다.
        MindMapManager.instance.GetID += 1;
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

    // 노드의 주제인 루트 노드에 대한 접근 프로퍼티
    public bool ROOTNODE
    {
        get { return rootNode; }
        set { rootNode = value; }
    }



}
