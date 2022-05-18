using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlSpec : Spec
{
    private float _force;
    
    public float Force
    {
        get => _force;
        set => _force = value;
    }

    // Constructor to make a random pearl.
    public PearlSpec()
    {
        this.Force = Random.Range(0.1f, 1f);
    }
    
    // Constructor to make a pearl based on the specified newForce.
    public PearlSpec(float newForce)
    {
        this.Force = newForce;
    }
}
