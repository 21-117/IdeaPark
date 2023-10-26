using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMindButton : MonoBehaviour
{
    public GameObject palette;
    public GameObject keyboard;
    public void OnOffPalette()
    {
        if (!palette.activeSelf)
        {
            palette.SetActive(true);
        }
        else
        {
            palette.SetActive(false);
        }
    }
    public void OnOffKeyboard()
    {
        if (!keyboard.activeSelf)
        {
            keyboard.SetActive(true);
        }
        else
        {
            keyboard.SetActive(false);
        }
    }
}
