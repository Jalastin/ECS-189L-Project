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

    public void Fire()
    {
        var velocity = new Vector2(this.VelocityX, this.VelocityY);
        this.GetComponent<Rigidbody2D>().AddForce(velocity);
    }
}
