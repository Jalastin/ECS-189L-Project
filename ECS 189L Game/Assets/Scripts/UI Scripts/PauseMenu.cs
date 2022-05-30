using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Resource: https://www.youtube.com/watch?v=JivuXdrIHK0
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;

    void Awake()
    {
        GameManager.OnGameStateChanged += this.OnStateChanged;
    }

    void OnDestroy()
    {
        // Need to unsubscribe if component is destroyed.
        GameManager.OnGameStateChanged -= this.OnStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        // Pause the game if the user presses the escape key.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.CurrentState == GameState.Paused)
            {
                // Resume game if wes press escape when it is already paused.
                GameManager.Instance.UpdateGameState(GameState.Playing);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.Paused);
            }
        }
    }

    public void ToggleResume()
    {
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }

    public void ResumeGame()
    {
        // Disable pause menu. 
        PauseMenuUI.SetActive(false);
        // Reset time scale back to the original value
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        // Enable pause menu. 
        PauseMenuUI.SetActive(true);
        // Freeze the game by stopping time.
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.UpdateGameState(GameState.Playing);
        // Reload level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleMainMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    public void LoadMainMenu()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
        // SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                this.LoadMainMenu();
                break;
            case GameState.Playing:
                this.ResumeGame();
                break;
            case GameState.Paused:
                this.PauseGame();
                break;
        }
    }
}
