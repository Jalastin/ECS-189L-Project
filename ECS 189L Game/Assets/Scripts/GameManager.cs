using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Lots of inspiration taken from https://www.youtube.com/watch?v=4I0vonyqMi8.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance 
    { 
        get;
        private set;
    }
    public GameState CurrentState 
    { 
        get;
        private set;
    }

    // Miscellaneous stats.
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

            // Notify only after 1 second has passed.
            if (value == 0f || (int)value - (int)previousTime > 0)
            {
                OnCompletionTimeChanged?.Invoke(this.completionTime);
            }
        }
    }

    // Audio properties.
    public bool VolumeChanged
    {
        get;
        set;
    }
    public float CurrentVolume
    {
        get;
        set;
    }

    // Events that other scripts can subscribe to.
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<int> OnPearlsThrownChanged;
    public static event Action<float> OnCompletionTimeChanged;

    void Awake()
    {
        // https://www.youtube.com/watch?v=5p2JlI7PV1w
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
            this.CurrentState = GameState.MainMenu;
            this.VolumeChanged = false;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        switch (this.CurrentState)
        {   
            case GameState.Playing:
                this.CompletionTime += Time.deltaTime;
                break;
        }
    }

    public void UpdateGameState(GameState newState)
    {
        // Redundant state change.
        if (newState == this.CurrentState)
        {
            return;
        }

        // Prob don't need this.
        var prevState = this.CurrentState;
        this.CurrentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene("MainMenu");
                Time.timeScale = 1f;
                break;

            case GameState.Starting:
                SceneManager.LoadScene("LevelDesign");
                this.CompletionTime = 0f;
                this.PearlsThrown = 0;
                this.UpdateGameState(GameState.Playing);
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 1f;
                break;

            case GameState.Won:
                break;

            case GameState.Credits:
                SceneManager.LoadScene("CreditsScene");
                break;
        }

        // Run respective callbacks of subscribed components.
        OnGameStateChanged?.Invoke(newState);
    }
}
