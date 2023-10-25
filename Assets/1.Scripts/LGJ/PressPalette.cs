using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressPalette : MonoBehaviour
{
    public GameObject palette;
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
}
