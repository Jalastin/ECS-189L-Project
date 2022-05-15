using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // forceCharge specifies how much force should be added
    // per frame while Fire1 is being held.
    [SerializeField] private float forceCharge = 0.1f;
    // Restrict how high the force can become.
    [SerializeField] private float maxForce = 10f;

    // force is how much force the new projectile should have.
    private float force;

    void Start()
    {
        this.force = 0;
    }

    void Update()
    {
        // Continue to charge force while the main fire button is being held down.
        if (Input.GetButton("Fire1"))
        {
            this.force += this.forceCharge;
            if (this.force >= this.maxForce)
            {
                this.force = this.maxForce;
            }
        }
        // Create the projectile with the specified force.
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log(this.force);
            this.GetComponent<ProjectileFactory>().Build(new ProjectileSpec(this.force));
            this.force = 0;
        }
    }
}
