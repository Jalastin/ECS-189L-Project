using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpriteFactory : Factory
{
    [SerializeField] private GameObject prefab;
    public GameObject Make(WindSpriteSpec newSpec)
    {
        // spawnPlace should be the local position of the windZone plus the offset specified in newSpec.
        var spawnPlace = (Vector2)this.transform.position + newSpec.Position;
        GameObject newGameObject = Instantiate(this.prefab, spawnPlace, Quaternion.identity);
        return newGameObject;
    }

    public GameObject Build(WindSpriteSpec newSpec)
    {
        var windSprite = Make(newSpec);
        return windSprite;
    }
    public GameObject GenerateRandomWindSprite()
    {
        WindSpriteSpec newSpec = new WindSpriteSpec();
        return Build(newSpec);
    }
}
