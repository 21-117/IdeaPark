using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveMapEvent : MonoBehaviour
{
    private List<XR_Bubble> objs;

    // Start is called before the first frame update
    void Awake()
    {
        objs = new List<XR_Bubble>();
        objs = FindObjectsOfType<XR_Bubble>().ToList();

        foreach (XR_Bubble obj in objs)
        {
            obj.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        StartCoroutine(LoadBubble());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadBubble()
    {
        yield return new WaitForSeconds(3f);
        
        foreach (XR_Bubble obj in objs)
        {
            obj.gameObject.SetActive(true);
        }
    }
}
