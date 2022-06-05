using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkZoneController : MonoBehaviour
{
    [SerializeField] private float darknessStrength = 1f;

    private GameObject player;

    private Color curColor;

    private bool inDarkZone;

    private float curDarkness;
    
    // Only begin increasing Darkness when the Player/Pearl enters the Dark Zone.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Pearl")
        {
            this.inDarkZone = true;
        }
    }

    // Only begin decreasing Darkness when the Player/Pearl exits the Dark Zone.
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.inDarkZone = false;
        }
    }

    void Start()
    {
        // Ensure that the Dark Zone does not collide with the player.
        this.player = GameObject.Find("Player_2");
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), this.player.GetComponent<Collider2D>());
        this.inDarkZone = false;
        this.curColor = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
        this.curDarkness = 0f;
    }

    void Update()
    {
        // If entering Dark Zone,
        // slowly increase the darkness by Time.DeltaTime
        // until it reaches or exceeds darknessStrength.
        if (this.inDarkZone)
        {
            this.curDarkness += Time.deltaTime;
            // Restrict curDarkness by darknessStrength.
            if (this.curDarkness >= this.darknessStrength)
            {
                this.curDarkness = this.darknessStrength;
            }
            this.GetComponent<SpriteRenderer>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, this.curDarkness);
        }

        // If entering Dark Zone,
        // slowly increase the darkness by Time.DeltaTime
        // until it reaches or exceeds 0.
        else
        {
            this.curDarkness -= Time.deltaTime;
            // Restrict curDarkness by darknessStrength.
            if (this.curDarkness <= 0f)
            {
                this.curDarkness = 0f;
            }
            this.GetComponent<SpriteRenderer>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, this.curDarkness);
        }
    }
}
