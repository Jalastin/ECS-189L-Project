using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditScreen : MonoBehaviour
{

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
