using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMindButton : MonoBehaviour
{
    public GameObject palette;
    public GameObject keyboard;
    public GameObject trash;
    public void OnOffButton(GameObject ui)
    {
        if (!ui.activeSelf)
        {
            ui.SetActive(true);
        }
        else
        {
            ui.SetActive(false);
        }
    }
}
