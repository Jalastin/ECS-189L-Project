using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CompletionTimeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerValueUI;
    
    void Awake()
    {
        GameManager.OnCompletionTimeChanged += OnTimerChange;
    }
    public void OnTimerChange(float time)
    {
        int timeValue = (int) time;
        // Update UI with the new value.
        TimerValueUI.text = timeValue.ToString();
    }
}
