using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    //material �迭�� ������
    [SerializeField] private Material[] skyboxMaterials;

    //Ư�� �迭�� �´� skybox�� �����Ű��
    public void ChangeSkybox(int index)
    {
        //skybox�� �����ϱ� ���ؼ� Ư�� index ������ �����Ѵ�.
        RenderSettings.skybox = skyboxMaterials[index];
    }
}