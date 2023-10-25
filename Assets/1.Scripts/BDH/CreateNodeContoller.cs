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

        // ������ ����� ���� ��ġ�� ����. 
        connectionLine.STARTPOS = this.transform;
        connectionLine.ENDPOSOBEJCT = endPosObject;

        if (indexObject)
        {
            // INDEXDISTAL�� ����� ���η������� �ĺ��ϱ� ���� True 
            connectionLine.INDEXDISTAL = true;
        }
       
    }

    // �ش� ��忡 ����Ǿ� �ִ� ���η����� ��带 ã�Ƽ� ���� 
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


    // INDEXDISTAL�� ����� ���η����� ��带 ã�Ƽ� ����.  
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
