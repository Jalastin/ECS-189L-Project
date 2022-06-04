using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    private int pearlsThrown = 0;
    public int PearlsThrown
    {
        get => pearlsThrown;
        set
        {
            pearlsThrown = value;
        }
    }
    private float completionTime = 0f;
    public float CompletionTime
    {
        get => completionTime;
        set
        {
            completionTime = value;
        }
    }
}
