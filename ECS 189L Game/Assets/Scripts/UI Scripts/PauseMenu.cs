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

    public void ToggleMainMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    public void RestartGame()
    {
        PauseMenuUI.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Playing);
        // Reload level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
