using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System;

public class ConnectionNodeController : MonoBehaviour
{

    private GameObject ConnectionLine;
    
    public static Action<GameObject, bool> nodeConnection; // ��� �� ���η������� ����
    public static Action<GameObject> destroyIndexTipConnection; // ���� indexTip ���η����� �ı� 
    public static Action<GameObject> destroyLineRenderer; // ��忡 ����� ���η����� �ı� 

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

        // endPosObject�� ������Ʈ�� �ڽ����� ���� ������Ʈ�� ����ȴ�.  
        LIneObject.transform.SetParent(endPosObject.transform);

        // NodeConnectLine�� ���ؼ� ����. 
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
    public void OnDestroyLineRenderer(GameObject deleteNode)
    {
        // �θ� LineRendererParent : ���η�����(�ڽ�)���� ������ �ִ� ������Ʈ  
        Transform LineRendererParent = deleteNode.transform.GetChild(1);

        for (int i = 0; i < LineRendererParent.childCount; i++)
        {
            // LineRendererParent�� ���η����� ������Ʈ���� ����. 
            Destroy(LineRendererParent.GetChild(i).gameObject);

        }
    }

    // INDEXDISTAL�� ����� ���η����� ��带 ã�Ƽ� ����.  
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
