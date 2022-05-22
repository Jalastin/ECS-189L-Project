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

    [SerializeField] private float forceMultipler = 10;

    // force is how much force the new pearl should have.
    private float force;

    private Vector3 mousePositionStart;

    private Vector3 mousePositionEnd;

    void Start()
    {
        this.force = 0;
    }

    void Update()
    {
        // Continue to charge force while the main fire button is being held down.
        if (Input.GetButtonDown("Fire1"))
        {
            this.mousePositionStart = GameObject.Find("Main Camera").GetComponent<CameraController>().MousePosition;
            // this.force += this.forceCharge;
            // if (this.force >= this.maxForce)
            // {
            //     this.force = this.maxForce;
            // }
        }
        // Create the pearl with the specified force.
        if (Input.GetButtonUp("Fire1"))
        {
            this.mousePositionEnd = GameObject.Find("Main Camera").GetComponent<CameraController>().MousePosition;
            var mouseDiff = this.mousePositionStart - this.mousePositionEnd;
            var mouseDistance = mouseDiff.magnitude;
            var mouseDirection = mouseDiff / mouseDistance;
            this.force = mouseDistance * this.forceMultipler;

            // Only run this code if there is no Pearl currently active.
            // This prevents multiple pearls from being thrown at once.
            if (GameObject.Find("Pearl(Clone)") == null)
            {
                this.GetComponent<PearlFactory>().Build(new PearlSpec(this.force, mouseDirection));
                this.force = 0;
            }
        }
    }
}
