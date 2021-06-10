using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class FadeController : MonoBehaviour
{
    public GameObject XRRig;
    public CanvasGroup canvasGroup;
    public bool isHidden;
    
    private float _fadeTime;
    
    private void Init()
    {
        _fadeTime = 4f;
    }

    void Start()
    {
        Init();
    }

    public void HideSelectEnter(SelectEnterEventArgs args)
    {
        StartCoroutine(FadeIn());
        isHidden = true;
        XRRig.SetActive(false);
    }


    IEnumerator FadeIn()
    {
        while ( canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha += 0.01f;
            yield return new WaitForSeconds(_fadeTime * 0.01f);
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() 
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= 0.01f;
            yield return new WaitForSeconds(_fadeTime * 0.01f);
        }
        
        isHidden = false;
        XRRig.SetActive(true);
    }
}