using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpriteSpec : Spec
{
    private float _force;
    
    public float Force
    {
        get => _force;
        set => _force = value;
    }

    public WindSpriteSpec()
    {
        this.Force = Random.Range(0.1f, 1f);
    }
    
    public WindSpriteSpec(float newForce)
    {
        this.Force = newForce;
    }
}
