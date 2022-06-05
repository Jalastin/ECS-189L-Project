using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    // Minimum time a WindSprite can spawn after the last WindSprite.
    [SerializeField] private float minWindSpriteSpawnTime = 1f;

    // Maximum time a WindSprite can spawn after the last WindSprite.
    [SerializeField] private float maxWindSpriteSpawnTime = 5f;

    // Time for the next WindSprite to spawn.
    private float spriteSpawnTime;

    // timeElapsed since last WindSprite spawn.
    private float timeElapsed;

    // Previous wind force necessary to remove the previous force added by the wind.
    private float prevWindForce;

    void OnTriggerStay2D(Collider2D other)
    {
        // Do not affect platforms or the player.
        if (other.gameObject.tag != "Platform" && other.gameObject.tag != "Player")
        {
            var windForce = this.GetComponent<ADSRManager>().FinalForce;
            // Take the current velocity of the GameObject.
            // Remove the previous wind force from it, then add the new one in.
            // This prevents from multiple wind forces from stacking on each other.
            var newVelocity = other.GetComponent<Rigidbody2D>().velocity;
            // Edge Case: only subtract the force if the windForce is nonzero.
            // Without this, objects get launched even though there is no wind.
            if (windForce != 0) {
                newVelocity -= new Vector2(this.prevWindForce, 0f);
            }
            newVelocity += new Vector2(windForce, 0f);
            this.prevWindForce = windForce;
            other.GetComponent<Rigidbody2D>().velocity = newVelocity;
        }
    }
    void Start()
    {
        this.GetComponent<WindSpriteFactory>().GenerateRandomWindSprite();
        this.spriteSpawnTime = Random.Range(this.minWindSpriteSpawnTime, this.maxWindSpriteSpawnTime);
        this.timeElapsed = 0f;
        this.prevWindForce = 0f;
    }

    void Update()
    {
        if (this.timeElapsed >= this.spriteSpawnTime)
        {
            this.GetComponent<WindSpriteFactory>().GenerateRandomWindSprite();
            this.spriteSpawnTime = Random.Range(1f, 5f);
            this.timeElapsed = 0f;
        }
        this.timeElapsed += Time.deltaTime;
    }
}
