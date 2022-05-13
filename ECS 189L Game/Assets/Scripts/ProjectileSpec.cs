using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ShieldSpec creates a random shield according to the specifications.
public class ProjectileSpec : Spec
{
    private float _force;
    
    public float Force
    {
        get => _force;
        set => _force = value;
    }

    // Constructor to make a random projectile.
    public ProjectileSpec()
    {
        this.Force = Random.Range(0.1f, 1f);
    }
    
    // Constructor to make a projectile based on the specified newForce.
    public ProjectileSpec(float newForce)
    {
        this.Force = newForce;
    }
}
