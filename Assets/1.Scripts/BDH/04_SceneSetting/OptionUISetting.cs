using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUISetting : MonoBehaviour
{
    public Scrollbar volumeSlider;
    public TMPro.TMP_Dropdown turnDropdown;
   // public SetTurnTypeFromPlayerPref turnTypeFromPlayerPref;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        //turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);

        //if (PlayerPrefs.HasKey("turn"))
        //    turnDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("turn"));
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

    //public void SetTurnPlayerPref(int value)
    //{
    //    PlayerPrefs.SetInt("turn", value);
    //    turnTypeFromPlayerPref.ApplyPlayerPref();
    //}
}