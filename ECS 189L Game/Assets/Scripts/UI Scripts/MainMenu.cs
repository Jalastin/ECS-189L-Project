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
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }

    public void LoadCredits()
    {
        GameManager.Instance.UpdateGameState(GameState.Credits);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
