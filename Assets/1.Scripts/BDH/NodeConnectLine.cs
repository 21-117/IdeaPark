using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnectLine : MonoBehaviour
{
    public LineRenderer lr;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    

    // Start is called before the first frame update
    void Start()
    {
        lr.positionCount = 3; 
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, pos1.position);
        lr.SetPosition(1, pos2.position);
        lr.SetPosition(2, pos3.position);
        
    }
}
