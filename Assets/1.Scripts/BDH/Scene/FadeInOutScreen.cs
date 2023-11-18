using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutScreen : MonoBehaviour
{
    public float fadeOutDuration = 2f;
    public float fadeInDuration = 7f;
    public Color fadeColor;
    private Renderer renderer; 
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        meshRenderer = GetComponent<MeshRenderer>();    
       
        // 처음 씬이 시작하면 7초간 서서히 FadeIn 효과 적용. 
        FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0, fadeInDuration, false);
    }

    public void FadeOut()
    {
        Fade(0, 1, fadeOutDuration, true);
    }

    public void Fade(float alphaIn, float alphaOut, float fadeDuration, bool isMeshRenderer)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, fadeDuration, isMeshRenderer));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut, float fadeDuration , bool isMeshRenderer) {
        float timer = 0;
        while(timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            renderer.material.SetColor("_Color", newColor); 

            timer += Time.deltaTime;
            yield return null; 
        }

        Color newColor_2 = fadeColor;
        newColor_2.a = alphaOut; 

        if(isMeshRenderer)
        {
            meshRenderer.enabled = true; 
        }
        else
        {
            meshRenderer.enabled = false;
        }
       
        renderer.material.SetColor("_Color", newColor_2);

    }

   
}
