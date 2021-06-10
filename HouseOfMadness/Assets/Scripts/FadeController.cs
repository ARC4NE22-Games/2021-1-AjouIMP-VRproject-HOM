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
    private AudioSource _audioSource;
    public AudioClip HideClip;

    private void Init()
    {
        _fadeTime = 3f;
        moveProvider = GameObject.Find("XR Rig").GetComponent<ContinuousMoveProviderBase>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = transform.Find("Panel").GetComponent<Image>();
        image.color = new Color(0, 0, 0, 1);
        canvasGroup.alpha = 0f;
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click0");
            isHidden = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("click1");
            isHidden = true;
        }
    }

    public void HideSelectEnter(SelectEnterEventArgs args)
    {
        if (!isHidden)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(HideClip);
            StartCoroutine(FadeIn());
        }
    }


    IEnumerator FadeIn()
    {
        isHidden = true;
        canvasGroup.alpha = 0f;
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