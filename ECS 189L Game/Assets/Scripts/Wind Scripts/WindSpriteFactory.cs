using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpriteFactory : Factory
{
    [SerializeField] private GameObject prefab;
    public GameObject Make(WindSpriteSpec newSpec)
    {
        var spawnPlace = this.transform.position; // change to random spawn
        GameObject newGameObject = Instantiate(this.prefab, spawnPlace, Quaternion.identity);
        return newGameObject;
    }

    public GameObject Build(WindSpriteSpec newSpec)
    {
        var windSprite = Make(newSpec);

        // Set the specs of the newly instantiated newGameObject
        // to the specs specified within newSpec.
        var windSpriteMotion = windSprite.GetComponent<WindSpriteMotion>();
        // Set the velcoity to the same force as WindZone's windForce.
        var windForce = this.GetComponent<WindZoneController>().WindForce;
        windSpriteMotion.VelocityX =  windForce;
        windSpriteMotion.VelocityY =  0f;
        return windSprite;
    }
    public GameObject GenerateRandomWindSprite()
    {
        WindSpriteSpec newSpec = new WindSpriteSpec();
        return Build(newSpec);
    }
}
