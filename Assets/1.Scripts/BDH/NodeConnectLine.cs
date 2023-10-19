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

    private LineRenderer lr;
 
    public Transform pos2;
    public Transform pos3;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        // 라인 렌더러 초기값 셋팅
        lr.positionCount = 2;
        
        lr.startColor = Color.black;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.endColor = Color.black ;
    }

    // Update is called once per frame
    void Update()
    {

        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, pos2.position);
        //lr.SetPosition(2, pos3.position);

       

    }
}
