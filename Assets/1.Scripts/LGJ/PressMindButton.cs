using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Net.WebRequestMethods;

public enum PressButton
{
    none,
    ai,
    palette,
    keyboard
}
public class PressMindButton : MonoBehaviour
{
    public PressButton state;
    public GameObject ai;
    public GameObject palette;
    public GameObject keyboard;
    public GameObject trash;
    private void Update()
    {
        if (PlayerInfo.localPlayer != null)
        {
            ai = PlayerInfo.localPlayer.ai;
            palette = PlayerInfo.localPlayer.palette;
            keyboard = PlayerInfo.localPlayer.keyboard;
        }
    }
    public void OnOffButton()
    {
        if (state == PressButton.ai)
        {
            ai.SetActive(true);
        }
        else
        {
            ai.SetActive(false);
        }

        if (state == PressButton.palette)
        {
            palette.SetActive(true);
        }
        else
        {
            palette.SetActive(false);
        }

        if (state == PressButton.keyboard)
        {
            keyboard.SetActive(true);
        }
        else
        {
            keyboard.SetActive(false);
        }
    }
}
