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
        get
        {   
            Debug.Log("getting manager");
            // return instance;
            if (instance == null)
            {
                Debug.Log("manager was null");
                instance = new GameManager();
            }
            return instance;
        }
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
        // Guard clause.
        if (newState == this.currentState)
        {
            return;
        }

        this.currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene("MainMenu");
                break;

            case GameState.Paused:
                break;

            case GameState.Playing:
                SceneManager.LoadScene("Justin Test Scene");
                break;

            case GameState.Credits:
                Debug.Log("loading credits scene");
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
