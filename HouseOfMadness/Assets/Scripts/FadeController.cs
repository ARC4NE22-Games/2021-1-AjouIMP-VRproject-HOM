using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class FadeController : MonoBehaviour
{
    public Image image;
    public bool isHidden;

    private GameObject _leftController, _rightController;
    private float _fadeTime;
    
    private void Init()
    {
        _leftController = GameObject.Find("XR Rig").transform.Find("Camera Offset").Find("LeftHand Controller").gameObject;
        _rightController = GameObject.Find("XR Rig").transform.Find("Camera Offset").Find("RightHand Controller").gameObject;
        _fadeTime = 2.5f;
        image.color = new Color(0, 0, 0, 0);
    }

    void Start()
    {
        Init();
    }

    public void HideSelectEnter(SelectEnterEventArgs args)
    {
        StartCoroutine(FadeIn());
        _leftController.SetActive(false);
        _rightController.SetActive(false);
        isHidden = true;
    }


    IEnumerator FadeIn()
    {
        while (image.color.a < 1.0f)
        {
            float alpha = image.color.a + 0.01f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForSeconds(_fadeTime * 0.01f);
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() 
    {
        while (image.color.a > 0)
        {
            float alpha = image.color.a - 0.01f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForSeconds(_fadeTime * 0.01f);
        }

        _leftController.SetActive(true);
        _rightController.SetActive(true);
        isHidden = false;
    }
}