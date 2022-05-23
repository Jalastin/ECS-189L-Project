using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Resource: https://www.youtube.com/watch?v=JivuXdrIHK0
public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject PauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        // Pause the game if the user presses the escape key.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                // Resume game if wes press escape when it is already paused.
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        // Disable pause menu. 
        PauseMenuUI.SetActive(false);
        // Reset time scale back to the original value
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void PauseGame()
    {
        // Enable pause menu. 
        PauseMenuUI.SetActive(true);
        // Freeze the game by stopping time.
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void RestartGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        // Reload level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
