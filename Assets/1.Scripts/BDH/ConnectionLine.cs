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
        // LineRenderer �� ���� ���� �� ����. 


        line = () =>
        {
            CreateLine();
        };
    }

    public void CreateLine()
    {

    }
}
