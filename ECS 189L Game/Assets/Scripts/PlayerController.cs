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

    // force is how much force the new pearl should have.
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
        // Create the pearl with the specified force.
        if (Input.GetButtonUp("Fire1"))
        {
            // Only run this code if there is no Pearl currently active.
            // This prevents multiple pearls from being thrown at once.
            if (GameObject.Find("Pearl(Clone)") == null)
            {
                this.GetComponent<PearlFactory>().Build(new PearlSpec(this.force));
                this.force = 0;
            }
        }
    }
}
