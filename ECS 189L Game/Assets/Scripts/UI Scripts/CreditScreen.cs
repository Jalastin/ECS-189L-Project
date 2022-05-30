using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditScreen : MonoBehaviour
{

    public void Back()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
        // SceneManager.LoadScene("MainMenu");
    }
}
