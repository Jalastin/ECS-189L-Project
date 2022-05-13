using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // forceCharge specifies how much force should be added
    // per frame while Fire1 is being held.
    [SerializeField] private float forceCharge = 0.01f;

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
            Debug.Log("charging");
            this.force += this.forceCharge;
        }
        // Create the projectile with the specified force.
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log(this.force);
            this.GetComponent<ProjectileFactory>().Build(new ProjectileSpec(this.force));
            Debug.Log("boom");
            this.force = 0;
        }
    }
}
