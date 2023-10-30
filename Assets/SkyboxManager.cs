using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    //material 배열을 만들자
    [SerializeField] private Material[] skyboxMaterials;

    //특정 배열에 맞는 skybox를 적용시키자
    public void ChangeSkybox(int index)
    {
        //skybox를 변경하기 위해서 특정 index 값으로 지정한다.
        RenderSettings.skybox = skyboxMaterials[index];
    }
}