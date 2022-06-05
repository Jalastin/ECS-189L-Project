using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class MainMenu : MonoBehaviour
{
    private Color curColor;

    void Awake()
    {
        this.curColor = GameObject.Find("Cursor").GetComponent<Image>().color;
    }
    
    // Background music should be playing by default
    void Start()
    {
        AudioListener.pause = false;
        if (Gamepad.current == null)
        {
            GameObject.Find("Cursor").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
        }
        else
        {
            GameObject.Find("Cursor").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 1f);
        }
    }

    public void ToggleStart()
    {
        GameManager.Instance.UpdateGameState(GameState.Starting);
    }

    public void ToggleCredits()
    {
        GameManager.Instance.UpdateGameState(GameState.Credits);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
