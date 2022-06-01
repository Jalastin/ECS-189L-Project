using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Lots of inspiration taken from https://www.youtube.com/watch?v=4I0vonyqMi8.
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    { 
        get => instance;
    }
    private GameState currentState = GameState.MainMenu;
    public GameState CurrentState 
    { 
        get => currentState;
    }
    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        // https://www.youtube.com/watch?v=5p2JlI7PV1w
        Debug.Log("manager awake!");
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateGameState(GameState newState)
    {
        Debug.Log(SceneManager.sceneCount);
        // Redundant state change.
        if (newState == this.currentState)
        {
            return;
        }

        var prevState = this.currentState;
        this.currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene("MainMenu");
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;

            case GameState.Playing:
                if (prevState != GameState.Paused)
                {
                    // No need to reload scene if we're just resuming from a
                    // pause.
                    SceneManager.LoadScene("LevelDesign");
                }
                Time.timeScale = 1f;
                break;

            case GameState.Credits:
                SceneManager.LoadScene("CreditsScene");
                break;
        }

        // Run respective callbacks of subscribed components.
        Debug.Log("invoking!");
        OnGameStateChanged?.Invoke(newState);
    }

    public void UpdateGameLevel()
    {
        
    }
}
