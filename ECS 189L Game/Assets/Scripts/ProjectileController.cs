using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMotion))]
public class ProjectileController : MonoBehaviour
{
    // teleportDelay is how much time the pearl should roll
    // before the player gets teleported.
    [SerializeField] private float teleportDelay = 0.3f;

    // playerOffset is how much higher the Player should be teleported
    // over the Pearl's original location.
    [SerializeField] private float playerOffset = 2f;

    // player just keeps track of the Player GameObject.
    private GameObject player;

    // Counter to keep track of timeElapsed since initial contact with a platform.
    private float timeElapsed;

    // Bool to check if the Pearl has collided with a GameObject.
    private bool _hasCollided;
    public bool HasCollided
    {
        get => _hasCollided;
        set => _hasCollided = value;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Prevent the pearl from colliding with the Player itself.
        if (other.gameObject.tag != "Player")
        {
            this.HasCollided = true;
        }
    }

    void Start()
    {
        this.timeElapsed = 0;
        this.HasCollided = false;
        // Destroy the Pearl for a given amount of time,
        // Remove once testing is finished
        Destroy(this.gameObject, 5f);
        this.GetComponent<ProjectileMotion>().Fire();
        // Ensure that the Pearl does not collide with the player.
        this.player = GameObject.Find("Player");
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), this.player.GetComponent<Collider2D>());
    }

    void Update()
    {
        if (this.HasCollided)
        {
            if (this.timeElapsed >= this.teleportDelay)
            {
                this.HasCollided = false;
                this.timeElapsed = 0;
                var pearlPosition = this.transform.position;
                // Set the Player's position to the Pearl's position,
                // plus the playerOffset to make it teleport "above" the pearl.
                this.player.transform.position = new Vector2(pearlPosition.x, pearlPosition.y + this.playerOffset);
                Destroy(this.gameObject);
            }
            this.timeElapsed += Time.deltaTime;
        }
    }
}
