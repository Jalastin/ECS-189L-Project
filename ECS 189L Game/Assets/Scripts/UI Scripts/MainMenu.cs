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

    public void ToggleStart()
    {
        GameManager.Instance.UpdateGameState(GameState.Starting);
    }

    public void ToggleCredits()
    {
        GameManager.Instance.UpdateGameState(GameState.Credits);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
