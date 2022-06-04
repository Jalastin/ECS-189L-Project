using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Restrict how high the force can become.
    [SerializeField] private float forceMax = 100f;

    // forceMultiplier is a scalar on the force of the new pearl.
    [SerializeField] private float forceMultipler = 10f;

    // Position of mouse when input button is first pressed.
    private Vector3 mousePositionStart;
    // Touch start for mobile.
    private Vector2 touchPositionStart;

    // Position of mouse when input button is being held.
    private Vector3 mousePositionEnd;
    // Touch end for mobile.
    private Vector2 touchPositionEnd;

    // mouseDiff is the Vector between the start and end mouse positions.
    private Vector3 mouseDiff;
    // mouseDiff for console.
    private Vector2 consoleMouseDiff;
    // mosueDiff for mobile.
    private Vector2 mobileMouseDiff;

    // mouseDistance is the magnitude of mouseDiff.
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
    private bool isThrow;
    private float throwTimer;
    private GameObject player;
    // Current scale of the player.
    private Vector3 scale;
    // Sound manager is used to generate sound effects when the player is charging their throw.
    private SoundEffectManager soundManager;

    // Console input implementation.
    private PlayerControls controls;
    private Vector2 move = new Vector2(0, 0);
    private bool isButtonPressed = false;

    void Awake()
    {
        // Activate the console controls.
        controls = new PlayerControls();
        // Input for moving the joystick.
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        // Button down input for shooting the projectile.
        controls.Gameplay.Button.performed += ctx => consoleButtonPressed();
    }
    // OnEnable and OnDisable to control to activate/deactivate the input system.
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void consoleButtonPressed() {
        if (this.pearlArcLine.enabled == true && this.consoleMouseDiff != new Vector2(0, 0))
        {
            this.isButtonPressed = true;
        }
    }
    
    void Start()
    {
        this.force = 0;
        this.pearlArcLine = this.gameObject.GetComponent<LineRenderer>();
        this.maxForceReached = false;
        this.player = GameObject.Find("Player_2");
        this.isThrow = false;
        this.throwTimer = 0.0f;
        this.soundManager = GameObject.Find("SoundManager").GetComponent<SoundEffectManager>();
        this.scale = this.gameObject.GetComponent<Rigidbody2D>().transform.localScale;
        // Because the Player starts facing left, for our game make it start facing right.
        this.gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(-this.scale.x, this.scale.y, this.scale.z);
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
        {
            // Don't allow any input while the game is paused.
            return;
        }

        // If running on mobile, do mobile input system.
        if (Input.touchCount > 0)
        {
            // Get the touch information.
            Touch touch = Input.GetTouch(0);
            // Get the location of the touch when it first touches the screen.
            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                this.touchPositionStart = new Vector2(pos.x, pos.y);
            }
            // Calculate the distance after touch gets removed.
            // Use this distance to calculate force and trajectory of the pearl.
            if (touch.phase == UnityEngine.TouchPhase.Moved)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                this.touchPositionEnd = new Vector2(pos.x, pos.y);

                this.mobileMouseDiff = this.touchPositionStart - this.touchPositionEnd;
                this.mouseDistance = this.mobileMouseDiff.magnitude;
                this.mouseDirection = this.mobileMouseDiff / this.mouseDistance;
                this.force = this.mouseDistance * this.forceMultipler;
                
                // Change sprite direction depending on touch position.
                // Edge case: only flip once they have pulled / there is a distance.
                if(this.mouseDirection.x < 0 && this.mouseDistance != 0) 
                {
                    // If pulling mouse to left, then have sprite face to the right.
                    this.gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(5, 5, 5);
                }
                else
                {
                    // If pulling mouse to right, then have sprite face to the left.
                    this.gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(-5, 5, 5);
                }

                // Restrict the force to be no bigger than forceMax.
                if (this.force >= this.forceMax)
                {
                    // Also limit the length of the pearl trajectory line
                    // to visually indicate when max force is being reached.
                    // We only want to set this change when forceMax is initially hit,
                    // which is why we set maxForcedReached to true until force is no longer at forceMax.
                    if (this.maxForceReached == false)
                    {
                        var maxDistance = this.touchPositionStart - this.touchPositionEnd;
                        this.maxMagnitude = maxDistance.magnitude;
                        this.maxForceReached = true;
                    }
                    var maxX = this.maxMagnitude * this.mouseDirection.x;
                    var maxY = this.maxMagnitude * this.mouseDirection.y;
                    this.mobileMouseDiff = new Vector2(maxX, maxY);

                    this.force = this.forceMax;
                }
                else
                {
                    this.maxForceReached = false;
                }

                // Draw the pearl trajectory based on drag direction and force.
                drawPearlArc();
            }
            // After touch is removed, launch the pearl.
            if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                // When releasing the pearl, turn off the pearl arc line.
                this.pearlArcLine.enabled = false;

                // Only fire the pearl if there is no pearl currently active.
                // This prevents multiple pearls from being thrown at once.
                // Also only fire if the force is non-zero.
                // (ie. they have actually dragged after pressing the screen).
                if (GameObject.Find("Pearl(Clone)") == null && this.force != 0)
                {
                    // Once player releases Fire1, start the throw animation.
                    this.isThrow = true;
                    player.GetComponent<Animator>().SetBool("Throw", this.isThrow);
                    this.soundManager.PlayProjectileReleaseSound();
                    this.GetComponent<PearlFactory>().Build(new PearlSpec(this.force, this.mouseDirection));
                    this.force = 0;
                }
            }
        }

        // If running on console, do console input system.
        // The second check is for when the joystick gets reset to 0, we still want to be
        // in the console logic to reset the consolMouseDiff. This will also prevent
        // a "ghost" arc line from lingering when there is no joystick input.
        else if (move != new Vector2(0, 0) || this.consoleMouseDiff != new Vector2(0, 0))
        {
            // Only shoot the pearl when the joystick is "active".
            if (move == new Vector2(0, 0))
            {
                this.consoleMouseDiff = new Vector2(0, 0);
            }

            // Every time joystick is moved, update the movement vector.
            this.consoleMouseDiff = this.consoleMouseDiff + new Vector2(-move.x, -move.y);

            // Calculate distance and force based on movement of left joystick.
            this.mouseDistance = this.consoleMouseDiff.magnitude;
            this.mouseDirection = this.consoleMouseDiff / this.mouseDistance;
            this.force = this.mouseDistance * this.forceMultipler;
            
            // Change sprite direction depending on launch direction.
            // Edge case: only flip once they have pulled / there is a distance.
            if(this.mouseDirection.x < 0 && this.mouseDistance != 0) 
            {
                // If aiming right, then have sprite face to the right.
                this.gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(5, 5, 5);
            }
            else
            {
                // If aiming left, then have sprite face to the left.
                this.gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(-5, 5, 5);
            }

            // Restrict the force to be no bigger than forceMax.
            if (this.force >= this.forceMax)
            {
                // Also limit the length of the pearl trajectory line
                // to visually indicate when max force is being reached.
                // We only want to set this change when forceMax is initially hit,
                // which is why we set maxForcedReached to true until force is no longer at forceMax.
                if (this.maxForceReached == false)
                {
                    var maxDistance = this.consoleMouseDiff;
                    this.maxMagnitude = maxDistance.magnitude;
                    this.maxForceReached = true;
                }
                var maxX = this.maxMagnitude * this.mouseDirection.x;
                var maxY = this.maxMagnitude * this.mouseDirection.y;
                this.consoleMouseDiff = new Vector2(maxX, maxY);

                this.force = this.forceMax;
            }
            else
            {
                this.maxForceReached = false;
            }

            // Draw the pearl trajectory based on drag direction and force.
            drawPearlArc();

            // When the console button is clicked, fire the pearl.
            if (this.isButtonPressed == true)
            {
                // When releasing the pearl, turn off the pearl arc line.
                this.pearlArcLine.enabled = false;

                // Only fire the pearl if there is no pearl currently active.
                // This prevents multiple pearls from being thrown at once.
                // Also only fire if the force is non-zero.
                // (ie. they have actually dragged after pressing button down).
                if (GameObject.Find("Pearl(Clone)") == null && this.force != 0)
                {
                    // Once player fires the pearl, start the throw animation.
                    this.isThrow = true;
                    player.GetComponent<Animator>().SetBool("Throw", this.isThrow);
                    this.soundManager.PlayProjectileReleaseSound();
                    this.GetComponent<PearlFactory>().Build(new PearlSpec(this.force, this.mouseDirection));
                    this.force = 0;
                }

                this.isButtonPressed = false;
            }
        }

        // If running on desktop, do desktop input system.
        else if (move == new Vector2(0, 0))
        {
            // When the input button is first pressed, set the start mouse position.
            if (Input.GetButtonDown("Fire1"))
            {
                this.mousePositionStart = GameObject.Find("Main Camera").GetComponent<CameraController>().MousePosition;
                this.soundManager.PlayChargingThrowSound();
            }

            // While the input button is being held, set the end mouse position, mouse direction, and force.
            if (Input.GetButton("Fire1"))
            {
                this.mousePositionEnd = GameObject.Find("Main Camera").GetComponent<CameraController>().MousePosition;
                this.mouseDiff = this.mousePositionStart - this.mousePositionEnd;
                this.mouseDistance = this.mouseDiff.magnitude;
                this.mouseDirection = this.mouseDiff / this.mouseDistance;
                this.force = this.mouseDistance * this.forceMultipler;
                
                // Change sprite direction depending on mouse position.
                // Edge case: only flip once they have pulled / there is a distance.
                if(this.mouseDirection.x < 0 && this.mouseDistance != 0) 
                {
                    // If pulling mouse to left, then have sprite face to the right.
                    this.gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(5, 5, 5);
                }
                else
                {
                    // If pulling mouse to right, then have sprite face to the left.
                    this.gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(-5, 5, 5);
                }

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
                // Also only fire if the force is non-zero.
                // (ie. they have actually dragged after pressing button down).
                if (GameObject.Find("Pearl(Clone)") == null && this.force != 0)
                {
                    // Once player releases Fire1, start the throw animation.
                    this.isThrow = true;
                    player.GetComponent<Animator>().SetBool("Throw", this.isThrow);
                    this.soundManager.PlayProjectileReleaseSound();
                    this.GetComponent<PearlFactory>().Build(new PearlSpec(this.force, this.mouseDirection));
                    this.force = 0;
                }
            }
        }

        // Allow a buffer between throwing and idling animations.
        if(this.isThrow)
        {
            // Count until 0.65sec, then set isThrow to false, which indicates the throw is done.
            if(throwTimer >= 0.65f) 
            {
                this.isThrow = false;
                this.throwTimer = 0.0f;
            }
            else
            {
                // If timer not reached yet, keep on incrementing.
                throwTimer += Time.deltaTime;
            }
        }
        else
        {
            // Go back to idle animation.
            player.GetComponent<Animator>().SetBool("Throw", this.isThrow);
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

        // If running on mobile, draw arc using mobile data.
        if (Input.touchCount > 0)
        {
            // Scale the mouseDiff by 2 / forceMultiplier, to not be too obstructive on the screen.
            var arcX = pearlSpawnPosition.x + (this.mobileMouseDiff.x / this.forceMultipler * 2);
            var arcY = pearlSpawnPosition.y + (this.mobileMouseDiff.y / this.forceMultipler * 2);
            this.pearlArcLine.SetPosition(1, new Vector2(arcX, arcY));
        }

        // If running on console, draw arc using console data.
        // Only draw the arc line when the joystick is active.
        else if (move != new Vector2(0, 0))
        {
            var arcX = pearlSpawnPosition.x + (this.consoleMouseDiff.x / this.forceMultipler * 2);
            var arcY = pearlSpawnPosition.y + (this.consoleMouseDiff.y / this.forceMultipler * 2);
            this.pearlArcLine.SetPosition(1, new Vector2(arcX, arcY));
        }

        // If running on desktop, draw arc using desktop data.
        else if (move == new Vector2(0, 0))
        {
            var arcX = pearlSpawnPosition.x + this.mouseDiff.x / this.forceMultipler * 2;
            var arcY = pearlSpawnPosition.y + this.mouseDiff.y / this.forceMultipler * 2;
            var arcZ = pearlSpawnPosition.z + this.mouseDiff.z / this.forceMultipler * 2;
            this.pearlArcLine.SetPosition(1, new Vector3(arcX, arcY, arcZ));
        }
    }
}
