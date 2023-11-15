using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_WORKSPACE); 
    }

}
