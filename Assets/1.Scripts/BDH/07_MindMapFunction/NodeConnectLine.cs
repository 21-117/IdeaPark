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


    [SerializeField]
    private bool indexDistal;

    private LineRenderer lr;
 

    // ���� ��ġ�� �׻� �θ��� ������Ʈ�� Pos�� �����´�. 
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

        //// ���� ������ �ʱⰪ ����
        lr.positionCount = 2;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;

    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject != null && endObject != null)
        {
            // ������ ���������� ���� 
            lr.SetPosition(0, startPos.position);
            lr.SetPosition(1, endObject.transform.position);
        }
      


    }
}
