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
            if (this.HasCollided == false)
            {
                // Slowly decrease the Y velocity over time (for a gravity effect).
                this.VelocityY -= this.gravity;
            }
            else
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.GetComponent<Rigidbody2D>().angularVelocity = 0;
                this.VelocityY = 0;
            }
            var velocity = new Vector2(this.VelocityX, this.VelocityY);
            this.GetComponent<Rigidbody2D>().AddForce(velocity);
        }
    }
}
