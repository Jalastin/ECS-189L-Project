using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlSpec : Spec
{
    // Pearls should be made with a specified force.
    private float _force;
    
    public float Force
    {
        get => _force;
        set => _force = value;
    }

    // Pearls should be thrown in a specified direction.
    private Vector3 _direction;

    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    // Constructor to make a random pearl.
    public PearlSpec()
    {
        this.Force = Random.Range(0.1f, 1f);
        var x = Random.Range(0f, 1f);
        var y = Random.Range(0f, 1f);
        var z = Random.Range(0f, 1f);
        this.Direction = new Vector3(x, y, z);
    }
    
    // Constructor to make a pearl based on the specified newForce.
    public PearlSpec(float newForce, Vector3 newDirection)
    {
        this.Force = newForce;
        this.Direction = newDirection;
    }
}
