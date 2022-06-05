using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpriteSpec : Spec
{
    // WindSpriteSpecs have a position offset of where it should spawn.
    private Vector2 _position;
    
    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public WindSpriteSpec()
    {
        // Get the size of the windZone.
        // Set the position to be in a random location in that zone.
        var x = GameObject.Find("Wind Zone").GetComponent<SpriteRenderer>().bounds.size.x;
        var xPos = Random.Range(-x/2f, x/2f);
        var y = GameObject.Find("Wind Zone").GetComponent<SpriteRenderer>().bounds.size.y;
        var yPos = Random.Range(-y/2f, y/2f);
        this.Position = new Vector2(xPos,yPos);
    }
    
    public WindSpriteSpec(Vector2 newPosition)
    {
        this.Position = newPosition;
    }
}
