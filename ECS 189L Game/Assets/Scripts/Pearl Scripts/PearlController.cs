using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PearlMotion))]
public class PearlController : MonoBehaviour
{
    // teleportDelay is how much time the pearl should roll
    // before the player gets teleported.
    [SerializeField] private float teleportDelay = 0.3f;

    // playerOffset is how much higher the Player should be teleported
    // over the Pearl's original location.
    [SerializeField] private float playerOffset = 2f;

    // player just keeps track of the Player Model GameObject.
    private GameObject player;

    // Counter to keep track of timeElapsed since initial contact with a platform.
    private float timeElapsed;

    // Sound manager is used to generate projectile sound effects.
    private SoundEffectManager soundManager;

    // Bool to check if the Pearl has collided with a GameObject.
    private bool _hasCollided;
    public bool HasCollided
    {
        get => _hasCollided;
        set => _hasCollided = value;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Play sound effect for projectile collision.
        this.soundManager.PlayProjectileCollisionSound();
        // Prevent the pearl from colliding with the Player itself.
        if (other.gameObject.tag != "Player")
        {
            // If the Pearl has collided with the crown, you win!
            if (other.gameObject.tag == "Crown")
            {
                GameManager.Instance.UpdateGameState(GameState.Won);
            }
            this.HasCollided = true;
        }
    }

    void Start()
    {
        // On initialization, fire the pearl.
        // It hasn't collided with anything yet.
        this.timeElapsed = 0;
        this.HasCollided = false;
        this.GetComponent<PearlMotion>().Fire();
        // Ensure that the Pearl does not collide with the player.
        this.player = GameObject.Find("Player_2");
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), this.player.GetComponent<Collider2D>());
        // Get sound manager object.
        this.soundManager = GameObject.Find("SoundManager").GetComponent<SoundEffectManager>();

    }

    void Update()
    {
        // If the Pearl is out of the world, reset the player to the start.
        if (this.transform.position.y <= -20f)
        {
            Destroy(this.gameObject);
            GameObject.Find("Player_2").transform.position = GameObject.Find("Player_2").GetComponent<PlayerController>().StartPoint;
            // Also move the camera back to the start as well.
            var StartPoint = GameObject.Find("Player_2").GetComponent<PlayerController>().StartPoint;
            GameObject.Find("Main Camera").GetComponent<Camera>().transform.position = new Vector3(StartPoint.x, StartPoint.y, -20f);
        }

        // If a collision has occured,
        // Start incrementing timeElapsed by Time.DeltaTime
        // until it equals or exceeds the teleport delay.
        if (this.HasCollided)
        {
            if (this.timeElapsed >= this.teleportDelay)
            {
                // Reset some of the other variables.
                this.HasCollided = false;
                this.timeElapsed = 0;
                var pearlPosition = this.transform.position;
                // Play sound effect.
                this.soundManager.PlayTeleportationSound();
                // Set the Player's position to the Pearl's position,
                // plus the playerOffset to make it teleport "above" the pearl.
                this.player.transform.position = new Vector2(pearlPosition.x, pearlPosition.y + this.playerOffset);
                Destroy(this.gameObject);
            }
            this.timeElapsed += Time.deltaTime;
        }
    }
}
