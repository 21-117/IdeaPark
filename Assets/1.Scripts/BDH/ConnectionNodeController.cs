using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System;

public class ConnectionNodeController : MonoBehaviour
{

    private GameObject ConnectionLine;
    
    public static Action<GameObject, bool> nodeConnection; // 노드 간 라인렌더러를 연결
    public static Action<GameObject> destroyIndexTipConnection; // 노드와 indexTip 라인렌더러 파괴 
    public static Action<GameObject> destroyLineRenderer; // 노드에 연결된 라인렌더러 파괴 

    void Start()
    {
        nodeConnection = (posObject, indexObject) =>
        {
            ConnectionNode(posObject, indexObject);
        };

        destroyIndexTipConnection = (deleteObject) =>
        {
            OnDestroyIndexTip(deleteObject);
        };

        destroyLineRenderer = (deleteLine) =>
        {
            OnDestroyLineRenderer(deleteLine); 

        };

    }

    public void ConnectionNode(GameObject endPosObject, bool indexObject)
    {
       
        ConnectionLine = Resources.Load<GameObject>("Prefabs/NodeConnectionLine");
        GameObject LIneObject = Instantiate(ConnectionLine, Vector3.zero, Quaternion.identity);

        // endPosObject의 오브젝트에 자식으로 라인 오브젝트가 저장된다.  
        LIneObject.transform.SetParent(endPosObject.transform);

        // NodeConnectLine를 통해서 연결. 
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
    public void OnDestroyLineRenderer(GameObject deleteNode)
    {
        // 부모 LineRendererParent : 라인렌더러(자식)들을 가지고 있는 오브젝트  
        Transform LineRendererParent = deleteNode.transform.GetChild(1);

        for (int i = 0; i < LineRendererParent.childCount; i++)
        {
            // LineRendererParent의 라인렌더러 오브젝트들을 삭제. 
            Destroy(LineRendererParent.GetChild(i).gameObject);

        }
    }

    // INDEXDISTAL과 연결된 라인렌더러 노드를 찾아서 삭제.  
    public void OnDestroyIndexTip(GameObject deleteNode)
    {

        for (int i = 0; i < deleteNode.transform.childCount; i++)
        {

            if (deleteNode.transform.GetChild(i).GetComponent<NodeConnectLine>().INDEXDISTAL == true)
            {
                Destroy(deleteNode.transform.GetChild(i).GetComponent<NodeConnectLine>().gameObject);
            }

        }

    }




}
