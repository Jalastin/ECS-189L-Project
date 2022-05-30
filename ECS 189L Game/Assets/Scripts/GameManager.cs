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
    private GameState currentState = GameState.Playing;
    public GameState CurrentState 
    { 
        get => currentState;
    }
    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Debug.Log("manager awake!");
        instance = this;
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
                break;
            case GameState.Paused:
                break;
            case GameState.Playing:
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
