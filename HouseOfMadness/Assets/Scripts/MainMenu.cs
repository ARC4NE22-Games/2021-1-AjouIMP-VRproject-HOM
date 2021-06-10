using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject light;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

    private void Update()
    {
        RotateLight();
    }

    void RotateLight()
    {
        light.transform.Rotate(new Vector3(-0.015f, -0.05f, 0));

    }

    public void Start_Menu()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Exit_Menu()
    {
        Application.Quit();
    }
}
