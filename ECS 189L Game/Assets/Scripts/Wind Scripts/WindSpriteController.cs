using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpriteController : MonoBehaviour
{
    // Time in seconds for the wind sprites to get destroyed.
    [SerializeField] private float timeToDie = 10f;

    void Start()
    {
        Destroy(this.gameObject, this.timeToDie);
    }

    void Update()
    {
        // Update the velocity of the wind sprite to the current force in ADSRManager.
        var windForce = GameObject.Find("Wind Zone").GetComponent<ADSRManager>().FinalForce;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(windForce, 0);
    }
}
