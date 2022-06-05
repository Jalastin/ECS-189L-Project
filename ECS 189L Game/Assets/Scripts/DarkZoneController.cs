using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkZoneController : MonoBehaviour
{
    // Determines how "Dark" the Dark Zone should be.
    // Because it determines the opacity of the zone,
    // it must be restricted from 0f to 1f.
    [Range(0f,1f)]
    [SerializeField] private float darknessStrength = 1f;

    // This is just a reference to the Player for easier use later.
    private GameObject player;

    // This contains the current color (ie. the darkness) of the Dark Zone.
    private Color curColor;

    // This flag determines if the Pearl/Player is in the Dark Zone or not.
    private bool inDarkZone;

    // This is the current strength of the Darkness.
    private float curDarkness;

    void Start()
    {
        // Ensure that the Dark Zone does not collide with the player.
        this.player = GameObject.Find("Player_2");
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), this.player.GetComponent<Collider2D>());
        // Player starts off not in the Dark Zone.
        // Get the current color and set it to 0f, ie transparent / no darkness.
        this.inDarkZone = false;
        this.curColor = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().color = new Color(this.curColor.r, this.curColor.g, this.curColor.b, 0f);
        this.curDarkness = 0f;
    }

    void Update()
    {
        // Simply check if the player is within the y area of the Dark Zone,
        // which in this level is currently from 300y to 560y.
        // Once within the zone, start increasing the Darkness.
        // If we had more time I would implement this with OnCollisionEnter + Exit instead.
        if (this.player.transform.position.y >= 300 && this.player.transform.position.y <= 560)
        {
            this.inDarkZone = true;
        }
        else
        {
            this.inDarkZone = false;
        }

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
