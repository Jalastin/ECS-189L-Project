using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        GameManager.OnGameStateChanged += OnStateChanged;
    }

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

    public void OnStateChanged(GameState state)
    {

    }
}
