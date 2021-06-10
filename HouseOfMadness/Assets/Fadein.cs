using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadein : MonoBehaviour
{
    public Image _Image;

    Color blackColor = Color.black;
    Color offColor = Color.clear;
    Color startColor = Color.black;
    Color targetColor = Color.black;

    private bool isOnTransition = false;
    float fadeTime = 0.5f;
    float delay = 0f;
    string nextScene = "";

    public void Fadeinout()
    {
        BlackOut(3f);
        BlackIn(3f);
    }
       

    public void BlackIn(float a_fadeTime = 0.5f, float a_delay = 0f)
    {
        Debug.Log("fadeIn");
        fadeTime = a_fadeTime;
        delay = a_delay;

        _Image.enabled = true;
        _Image.color = blackColor;
        startColor = blackColor;
        targetColor = offColor;
        _Image.raycastTarget = false;

        if (isOnTransition)
            StopCoroutine("UpdateColorCoroutine");

        StartCoroutine("UpdateColorCoroutine");
    }

    public void BlackOut(float a_fadeTime = 0.5f, float a_delay = 0f, string a_nextScene = "")
    {
        Debug.Log("fadeout");
        fadeTime = a_fadeTime;
        delay = a_delay;
        nextScene = a_nextScene;

        _Image.enabled = true;
        startColor = _Image.color;
        targetColor = blackColor;
        _Image.raycastTarget = true;

        if (isOnTransition)
            StopCoroutine("UpdateColorCoroutine");

        StartCoroutine("UpdateColorCoroutine");
    }

    IEnumerator UpdateColorCoroutine()
    {
        isOnTransition = true;

        if (delay != 0)
            yield return new WaitForSeconds(delay);

        float t = 0;
        while (t < 1)
        {
            _Image.color = Color.Lerp(startColor, targetColor, t);
            t += Time.deltaTime / fadeTime;
            yield return new WaitForEndOfFrame();
        }

        if (targetColor == Color.clear) // 시간을 지났을 경우! 즉 트랜지션 끝남
            _Image.enabled = false;


        isOnTransition = false;
    }
}