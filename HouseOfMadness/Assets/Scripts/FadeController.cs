using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class FadeController : MonoBehaviour
{
    public bool isHidden;
    
    private Image image;
    private ContinuousMoveProviderBase moveProvider;
    private CanvasGroup canvasGroup;
    private float _fadeTime;
    
    private void Init()
    {
        _fadeTime = 3f;
        moveProvider = GameObject.Find("XR Rig").GetComponent<ContinuousMoveProviderBase>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = transform.Find("Panel").GetComponent<Image>();
        image.color = new Color(0, 0, 0, 1);
        canvasGroup.alpha = 0f;
    }

    void Start()
    {
        Init();
    }
    
    public void HideSelectEnter(SelectEnterEventArgs args)
    {
        if (!isHidden)
        {
            StartCoroutine(FadeIn());
        }
    }


    IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0f;
        isHidden = true;
        moveProvider.moveSpeed = 0f;   
        
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
        moveProvider.moveSpeed = 2f;
    }
}