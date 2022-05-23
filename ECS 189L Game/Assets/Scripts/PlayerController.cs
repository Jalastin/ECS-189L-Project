using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Restrict how high the force can become.
    [SerializeField] private float forceMax = 100f;

    // forceMultiplier is a scalar on the force of the new pearl.
    [SerializeField] private float forceMultipler = 10f;

    // Position of mouse when input button is first pressed;
    private Vector3 mousePositionStart;

    // Position of mouse when input button is being held;
    private Vector3 mousePositionEnd;

    // mouseDiff is the Vector between the start and end mouse positions.
    private Vector3 mouseDiff;

    // mouseDistance is the magnitude of mouseDiff;
    private float mouseDistance;

    // mouseDirection is the actual direction of the mouseDiff vector.
    private Vector3 mouseDirection;

    // force is how much force the new pearl should have.
    private float force;

    // LineRenderer used to draw the pearl arc when throwing a new pearl.
    private LineRenderer pearlArcLine;
    
    // maxMagnitude is the maximum length the pearlArcLine can get.
    private float maxMagnitude;
    
    // Flag to determine if the force has exceeded forceMax.
    private bool maxForceReached;

    void Start()
    {
        this.force = 0;
        this.pearlArcLine = this.gameObject.GetComponent<LineRenderer>();
        this.maxForceReached = false;
    }

    void Update()
    {
        // When the input button is first pressed, set the start mouse position.
        if (Input.GetButtonDown("Fire1"))
        {
            this.mousePositionStart = GameObject.Find("Main Camera").GetComponent<CameraController>().MousePosition;
        }

        // While the input button is being held, set the end mouse position, mouse direction, and force.
        if (Input.GetButton("Fire1"))
        {
            this.mousePositionEnd = GameObject.Find("Main Camera").GetComponent<CameraController>().MousePosition;
            this.mouseDiff = this.mousePositionStart - this.mousePositionEnd;
            this.mouseDistance = this.mouseDiff.magnitude;
            this.mouseDirection = this.mouseDiff / this.mouseDistance;
            this.force = this.mouseDistance * this.forceMultipler;

            // Restrict the force to be no bigger than forceMax.
            if (this.force >= this.forceMax) 
            {
                // Also limit the length of the pearl trajectory line
                // to visually indicate when max force is being reached.
                // We only want to set this change when forceMax is initially hit,
                // which is why we set maxForcedReached to true until force is no longer at forceMax.
                if (this.maxForceReached == false)
                {
                    var maxDistance = mousePositionStart - mousePositionEnd;
                    this.maxMagnitude = maxDistance.magnitude;
                    this.maxForceReached = true;
                }
                var maxX = this.maxMagnitude * this.mouseDirection.x;
                var maxY = this.maxMagnitude * this.mouseDirection.y;
                var maxZ = this.maxMagnitude * this.mouseDirection.z;
                this.mouseDiff = new Vector3(maxX, maxY, maxZ);

                this.force = this.forceMax;
            }            
            else
            {
                this.maxForceReached = false;
            }
            
            // Draw the pearl trajectory based on drag direction and force.
            drawPearlArc();
        }
        
        // When the input button is let go, fire the pearl.
        if (Input.GetButtonUp("Fire1"))
        {
            // When releasing the pearl, turn off the pearl arc line.
            this.pearlArcLine.enabled = false;

            // Only fire the pearl if there is no pearl currently active.
            // This prevents multiple pearls from being thrown at once.
            if (GameObject.Find("Pearl(Clone)") == null)
            {
                this.GetComponent<PearlFactory>().Build(new PearlSpec(this.force, this.mouseDirection));
                this.force = 0;
            }
        }
    }

    void drawPearlArc() 
    {
        // Visually depict the mouseDiff, starting from Pearl Spawn.
        var pearlSpawnPosition = GameObject.Find("Pearl Spawn").transform.position;
        this.pearlArcLine.enabled = true;
        this.pearlArcLine.positionCount = 2;
        this.pearlArcLine.useWorldSpace = true;
        this.pearlArcLine.SetPosition(0, pearlSpawnPosition);

        // Scale the mouseDiff by 2 / forceMultiplier, to not be too obstructive on the screen.
        var arcX = pearlSpawnPosition.x + this.mouseDiff.x / this.forceMultipler * 2;
        var arcY = pearlSpawnPosition.y + this.mouseDiff.y / this.forceMultipler * 2;
        var arcZ = pearlSpawnPosition.z + this.mouseDiff.z / this.forceMultipler * 2;
        this.pearlArcLine.SetPosition(1, new Vector3(arcX, arcY, arcZ));
    }
}
