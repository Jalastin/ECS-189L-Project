using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public GameObject EndScreenUI;
    [SerializeField] TextMeshProUGUI PearlsThrownStatsUI;
    [SerializeField] TextMeshProUGUI CompletionTimeUI;

    void Awake()
    {
        GameManager.OnGameStateChanged += this.OnStateChanged;
    }

    void OnDestroy()
    {
        // Need to unsubscribe if component is destroyed.
        GameManager.OnGameStateChanged -= this.OnStateChanged;
    }

    public void ToggleMainMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    public void ToggleRestart()
    {
        GameManager.Instance.UpdateGameState(GameState.Starting);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnStateChanged(GameState state)
    {
        if (state == GameState.Won)
        {
            // Enable end screen refresh
            EndScreenUI.SetActive(true);
            PearlsThrownStatsUI.text = GameManager.Instance.PearlsThrown.ToString();
            CompletionTimeUI.text = GameManager.Instance.CompletionTime.ToString();
        }
    }
}
