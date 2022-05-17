using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMotion : MonoBehaviour
{
    private float _velocityX = 0f;

    public float VelocityX
    {
        get => _velocityX;
        set => _velocityX = value;
    }

    private float _velocityY = 0f;
    public float VelocityY
    {
        get => _velocityY;
        set => _velocityY = value;
    }

    [SerializeField] private float gravity = -10f;

    private bool hasFired;
    // HasCollided is set in ProjectileController.
    private bool _hasCollided;
    public bool HasCollided
    {
        get => _hasCollided;
        set => _hasCollided = value;
    }

    public void Fire()
    {
        var velocity = new Vector2(this.VelocityX, this.VelocityY);
        this.GetComponent<Rigidbody2D>().AddForce(velocity);
        this.hasFired = true;
    }

    void Start()
    {
        this.hasFired = false;
        this.HasCollided = false;
    }

    void Update()
    {
        if (this.hasFired)
        {
            // // While the pearl is still mid-air / hasn't collided with anything yet.
            // if (this.HasCollided == false)
            // {
            //     // Slowly decrease the Y velocity over time (for a gravity effect).
            //     this.VelocityY -= this.gravity;
            // }
            // // If the pearl has collided with something.
            // else
            // {
            //     // Strip all velocity from the Rigidbody.
            //     this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //     this.GetComponent<Rigidbody2D>().angularVelocity = 0;
            //     // Set VelocityY to 0, so that it only moves on the x-axis (ie. rolling).
            //     this.VelocityY = 0;
            // }
            // Slowly decrease the Y velocity over time (for a gravity effect).
            this.VelocityY -= this.gravity;
            var velocity = new Vector2(this.VelocityX, this.VelocityY);
            this.GetComponent<Rigidbody2D>().AddForce(velocity);
        }
    }
}
