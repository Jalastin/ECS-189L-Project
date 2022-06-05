using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class CreditScreen : MonoBehaviour
{
    private Color curColor;

    void Awake()
    {
        this.curColor = GameObject.Find("Cursor").GetComponent<Image>().color;
    }

    void Start()
    {
        if (Gamepad.current == null)
        {
            GameObject.Find("Cursor").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
        }
        else
        {
            GameObject.Find("Cursor").GetComponent<Image>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 1f);
        }
    }

    public void Back()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
}
