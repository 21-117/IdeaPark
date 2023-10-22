using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor.SearchService;
using UnityEngine;
using System; 

public class CreataNodeConnection : MonoBehaviour
{

    private GameObject obj;
    public static Action<Transform, bool> connection;
    public static Action destroyConnection; 

    void Start()
    {
        connection = (pos, indexObject) =>
        {
            ConnectionNode(pos, indexObject);
        };

        destroyConnection = () =>
        {
            OnDestroyindexObject();
        };

    }

    public void ConnectionNode(Transform endPos, bool indexObject)
    {

        obj = Resources.Load<GameObject>("Prefabs/NodeConnectionLine");
        GameObject LIneObject = Instantiate(obj, Vector3.zero, Quaternion.identity);
        LIneObject.transform.SetParent(this.transform);
        NodeConnectLine connectionLine = LIneObject.GetComponent<NodeConnectLine>();
        // 연결할 노드의 연결 위치를 지정. 
        connectionLine.STARTPOS = this.transform;
        connectionLine.ENDPOS = endPos;

        if (indexObject)
        {
            // INDEXDISTAL과 연결된 라인렌더러를 식별하기 위해 True 
            connectionLine.INDEXDISTAL = true;


        }
        

    }

    public void OnDestroyindexObject()
    {
        for(int i = 0; i < transform.childCount; i++) { 
            
         
            if(transform.GetChild(i).GetComponent<NodeConnectLine>().INDEXDISTAL == true)
            {
                Destroy(transform.GetChild(i).GetComponent<NodeConnectLine>().gameObject);
            }

        }
        
    }




}
