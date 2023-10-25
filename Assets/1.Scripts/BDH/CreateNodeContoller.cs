using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor.SearchService;
using UnityEngine;
using System; 

public class CreateNodeContoller : MonoBehaviour
{

    private GameObject obj;
    public static Action<GameObject, bool> connection;
    public static Action destroyConnection; 

    void Start()
    {
        connection = (posObject, indexObject) =>
        {
            ConnectionIndexNode(posObject, indexObject);
        };

        destroyConnection = () =>
        {
            OnDestroyindexObject();
        };

    }

    public void ConnectionIndexNode(GameObject endPosObject, bool indexObject)
    {
        obj = Resources.Load<GameObject>("Prefabs/NodeConnectionLine");
        GameObject LIneObject = Instantiate(obj, Vector3.zero, Quaternion.identity);
        LIneObject.transform.SetParent(this.transform);
        NodeConnectLine connectionLine = LIneObject.GetComponent<NodeConnectLine>();

        // 연결할 노드의 연결 위치를 지정. 
        connectionLine.STARTPOS = this.transform;
        connectionLine.ENDPOSOBEJCT = endPosObject;

        if (indexObject)
        {
            // INDEXDISTAL과 연결된 라인렌더러를 식별하기 위해 True 
            connectionLine.INDEXDISTAL = true;
        }
       
    }

    // 해당 노드에 연결되어 있는 라인렌더러 노드를 찾아서 삭제 
    public void OnDestroyNodeObject(GameObject deleteNode)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<NodeConnectLine>().ENDPOSOBEJCT == deleteNode)
            {
                Destroy(transform.GetChild(i).GetComponent<NodeConnectLine>().gameObject);
            }
        }
    }


    // INDEXDISTAL과 연결된 라인렌더러 노드를 찾아서 삭제.  
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
