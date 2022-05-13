using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMotion : MonoBehaviour
{
    [SerializeField] private Vector3 MuzzleVelocity = new Vector2(0.1f, 0f);

    public void Fire()
    {
        this.GetComponent<Rigidbody2D>().AddForce(this.MuzzleVelocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

}
