using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMotion : MonoBehaviour
{
    [SerializeField] private Vector2 _velocity = new Vector2(0.1f, 0f);

    public Vector2 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    public void Fire()
    {
        this.GetComponent<Rigidbody2D>().AddForce(this.Velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

}
