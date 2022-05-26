using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lots of inspiration taken from https://www.youtube.com/watch?v=4I0vonyqMi8.
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    { 
        get => instance;
    }
    private GameState currentState;
    public GameState CurrentState 
    { 
        get => currentState;
    }
    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        this.currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Paused:
                break;
            case GameState.Playing:
                break;
        }

        // Run respective callbacks of subscribed components.
        OnGameStateChanged?.Invoke(newState);
    }
}
