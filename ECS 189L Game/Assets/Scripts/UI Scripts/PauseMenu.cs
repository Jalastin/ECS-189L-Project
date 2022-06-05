using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

// Resource: https://www.youtube.com/watch?v=JivuXdrIHK0
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;

    private PlayerControls controls;
    private bool isButtonPressed = false;
    private Color curColor;

    void Awake()
    {
        GameManager.OnGameStateChanged += this.OnStateChanged;

        controls = new PlayerControls();
        controls.Gameplay.Pause.performed += ctx => startButtonPressed();
        this.curColor = GameObject.Find("CursorPause").GetComponent<Image>().color;
        GameObject.Find("CursorPause").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    void startButtonPressed() {
        this.isButtonPressed = true;
    }

    void OnDestroy()
    {
        // Need to unsubscribe if component is destroyed.
        GameManager.OnGameStateChanged -= this.OnStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        // Pause the game if the user presses the escape key if they are not in the end screen.
        // Or, pause the game if they press the start button on the gamepad.
        if ((Input.GetKeyDown(KeyCode.Escape) || this.isButtonPressed) && GameManager.Instance.CurrentState != GameState.Won)
        {
            if (GameManager.Instance.CurrentState == GameState.Paused)
            {
                // Resume game if we press escape when it is already paused.
                GameManager.Instance.UpdateGameState(GameState.Playing);
                GameObject.Find("CursorPause").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.Paused);
                if (Gamepad.current == null)
                {
                    GameObject.Find("CursorPause").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
                }
                else
                {
                    GameObject.Find("CursorPause").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 1f);
                }
                this.isButtonPressed = false;
            }
        }
    }

    public void ToggleResume()
    {
        GameManager.Instance.UpdateGameState(GameState.Playing);
        GameObject.Find("CursorPause").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
    }

    public void ToggleMainMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    public void TogglePauseMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.Paused);
        if (Gamepad.current == null)
        {
            GameObject.Find("CursorPause").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
        }
        else
        {
            GameObject.Find("CursorPause").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 1f);
        }
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
        switch (state)
        {
            case GameState.Playing:
                // Disable pause menu. 
                PauseMenuUI.SetActive(false);
                break;

            case GameState.Paused:
                // Enable pause menu. 
                PauseMenuUI.SetActive(true);
                break;
        }
    }
}
