using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Background music should be playing by default
    void Start()
    {
        AudioListener.pause = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Justin Test Scene");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
