using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start_Menu()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Exit_Menu()
    {
        Application.Quit();
    }
}
