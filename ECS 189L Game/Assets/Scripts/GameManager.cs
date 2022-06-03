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
    private GameStats stats;
    public int PearlsThrown
    {
        get => stats.PearlsThrown;
        set
        {
            this.stats.PearlsThrown = value;
            OnPearlsThrownChanged?.Invoke(this.stats.PearlsThrown);
        }
    }
    public float CompletionTime
    {
        get => this.stats.CompletionTime;
    }
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<int> OnPearlsThrownChanged;
    public static event Action<float> OnCompletionTimeChanged;

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
            stats = new GameStats();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        switch (this.currentState)
        {
            case GameState.Starting:
                this.stats.CompletionTime = 0f;
                break;
            
            case GameState.Playing:
                this.stats.CompletionTime += Time.deltaTime;
                OnCompletionTimeChanged?.Invoke(this.stats.CompletionTime);
                break;
        }
    }

    public void UpdateGameState(GameState newState)
    {
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

            case GameState.Starting:
                SceneManager.LoadScene("LevelDesign");
                this.UpdateGameState(GameState.Playing);
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;

            case GameState.Playing:
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
}
