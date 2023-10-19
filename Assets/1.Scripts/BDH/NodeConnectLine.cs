using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnectLine : MonoBehaviour
{
    // ��忡�� ���� �� ���η������� ����.
    // 1. Resources.Load()�� ���η����� ������Ʈ�� ������ �ִ� �� ������Ʈ�� ����.
    // 2. ������ �����ؾ� �� �� -> Instatiate() �� �ϰ�, ���η����� startPos, endPos�� �����Ѵ�. 
    // 3. Instatiate()�� ���η����� ����� �ڽ����� �����Ѵ�. 
    // 4. ���࿡ ���η������� �����ϰ� �ʹٸ�,
    // 5. �θ��� ��带 ã�Ƽ� �ش� �����ϰ� ���� ���η����� ������Ʈ�� �����Ѵ�. 

    private LineRenderer lr;
 
    public Transform pos2;
    public Transform pos3;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        // ���� ������ �ʱⰪ ����
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
