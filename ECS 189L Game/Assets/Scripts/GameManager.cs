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
    private int pearlsThrown;
    public int PearlsThrown
    {
        get => this.pearlsThrown;
        set
        {
            this.pearlsThrown = value;
            OnPearlsThrownChanged?.Invoke(this.pearlsThrown);
        }
    }
    private float completionTime;
    public float CompletionTime
    {
        get => this.completionTime;
        private set
        {
            var previousTime = this.completionTime;
            this.completionTime = value;
            if (Mathf.Floor(previousTime) != Mathf.Floor(this.completionTime))
            {
                OnCompletionTimeChanged?.Invoke(this.completionTime);
            }
        }
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
            // stats = new GameStats();
            instance = this;
            DontDestroyOnLoad(gameObject);
            // OnCompletionTimeChanged += OnTimeChanged;
        }
    }

    void Update()
    {
        switch (this.currentState)
        {
            case GameState.Starting:
                this.CompletionTime = 0f;
                this.PearlsThrown = 0;
                break;
            
            case GameState.Playing:
                this.CompletionTime += Time.deltaTime;
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

    // public void OnTimeChanged(float time)
    // {
    //     Debug.Log(time);
    // }
}
