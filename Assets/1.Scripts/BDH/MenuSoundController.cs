using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundController : MonoBehaviour
{
    private void OnEnable()
    {
        SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_CREATE);
    }

    private void OnDisable()
    {
        SoundManager.instance.PlaySFX(SoundManager.ESFX.SFX_UI_CREATE);
    }
}
