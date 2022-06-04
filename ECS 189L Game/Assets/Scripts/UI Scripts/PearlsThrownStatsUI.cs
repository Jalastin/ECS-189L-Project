using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PearlsThrownStatsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI StatsUI;

    void Awake()
    {
        GameManager.OnPearlsThrownChanged += OnPearlsThrownChangedUI;
    }

    // This function is called to update the UI whenever a pearl is thrown
    public void OnPearlsThrownChangedUI(int pearlsThrown)
    {
        // Update UI with the new value.
        StatsUI.text = pearlsThrown.ToString();
    }
}
