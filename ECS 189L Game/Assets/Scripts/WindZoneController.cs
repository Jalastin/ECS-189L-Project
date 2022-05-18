using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    // Magnitude / force of how hard the wind is blowing.
    [SerializeField] private float windForce = -1f;
    void OnTriggerStay2D(Collider2D other)
    {
        // Do not affect platforms.
        if (other.gameObject.tag != "Platform")
        {
            var vector = new Vector2 (this.windForce, 0f);
            other.GetComponent<Rigidbody2D>().AddForce(vector);
        }
    }
}
