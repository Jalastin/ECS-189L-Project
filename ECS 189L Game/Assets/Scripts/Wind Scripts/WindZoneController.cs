using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    // Magnitude / force of how hard the wind is blowing.
    [SerializeField] private float _windForce = -1f;
    public float WindForce
    {
        get => _windForce;
        set => _windForce = value;
    }

    // Minimum time a WindSprite can spawn after the last WindSprite.
    [SerializeField] private float minWindSpriteSpawnTime = 1f;

    // Maximum time a WindSprite can spawn after the last WindSprite.
    [SerializeField] private float maxWindSpriteSpawnTime = 5f;

    // Time for the next WindSprite to spawn.
    private float spriteSpawnTime;

    // timeElapsed since last WindSprite spawn.
    private float timeElapsed;

    void OnTriggerStay2D(Collider2D other)
    {
        // Do not affect platforms.
        if (other.gameObject.tag != "Platform")
        {
            var vector = new Vector2 (this.WindForce, 0f);
            other.GetComponent<Rigidbody2D>().AddForce(vector);
        }
    }
    void Start()
    {
        this.GetComponent<WindSpriteFactory>().GenerateRandomWindSprite();
        this.spriteSpawnTime = Random.Range(this.minWindSpriteSpawnTime, this.maxWindSpriteSpawnTime);
        this.timeElapsed = 0f;
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
