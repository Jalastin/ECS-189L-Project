using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public GameObject EndScreenUI;
    [SerializeField] TextMeshProUGUI PearlsThrownStatsUI;
    [SerializeField] TextMeshProUGUI CompletionTimeUI;

    private PlayerControls controls;
    private Color curColor;

    void Awake()
    {
        GameManager.OnGameStateChanged += this.OnStateChanged;

        // this.curColor = GameObject.Find("CursorEnd").GetComponent<Image>().color;
        // GameObject.Find("CursorEnd").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
    }

    void OnDestroy()
    {
        // Need to unsubscribe if component is destroyed.
        GameManager.OnGameStateChanged -= this.OnStateChanged;
    }

    public void ToggleMainMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
        // GameObject.Find("CursorPause").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 1f);
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
