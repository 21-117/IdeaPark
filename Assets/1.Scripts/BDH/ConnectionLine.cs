using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class ConnectionLine : MonoBehaviour
{
    public static Action line;
    private LineRenderer lr; 

    // Start is called before the first frame update
    void Start()
    {
        // LineRenderer 에 대한 생성 및 셋팅. 


        line = () =>
        {
            CreateLine();
        };
    }

    public void CreateLine()
    {

    }
}
