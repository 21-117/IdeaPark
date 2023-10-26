using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnectLine : MonoBehaviour
{
    // 노드에서 여러 개 라인렌더러를 대응.
    // 1. Resources.Load()에 라인렌더러 컴포넌트를 가지고 있는 빈 오브젝트를 생성.
    // 2. 라인을 생성해야 할 때 -> Instatiate() 를 하고, 라인렌더러 startPos, endPos로 설정한다. 
    // 3. Instatiate()한 라인렌더러 노드의 자식으로 설정한다. 
    // 4. 만약에 라인렌더러를 삭제하고 싶다면,
    // 5. 부모인 노드를 찾아서 해당 삭제하고 싶은 라인렌더러 오브젝트를 삭제한다. 


    [SerializeField]
    private bool indexDistal;

    private LineRenderer lr;
 

    // 시작 위치는 항상 부모의 오브젝트의 Pos를 가져온다. 
    private Transform startPos; 
    private GameObject endObject;

    public bool INDEXDISTAL
    {
        get { return indexDistal; }
        set { indexDistal = value; }
    }

    public Transform STARTPOS
    {
        get { return startPos; }
        set { startPos = value; }   
    }

    public GameObject ENDPOSOBEJCT
    {
        get { return endObject; }
        set { endObject = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        //// 라인 렌더러 초기값 셋팅
        lr.positionCount = 2;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;

    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject != null && endObject != null)
        {
            // 라인을 지속적으로 연결 
            lr.SetPosition(0, startPos.position);
            lr.SetPosition(1, endObject.transform.position);
        }
      


    }
}
