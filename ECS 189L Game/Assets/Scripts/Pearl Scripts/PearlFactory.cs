using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlFactory : Factory
{
    // Serialized reference to the pearl prefab that should be produced.
    [SerializeField] private GameObject prefab;
    // Serialized reference to where pearls should spawn on the Player.
    [SerializeField] private GameObject pearlSpawn;

    public GameObject Make()
    {
        // Spawn the new Pearl at pearlSpawn.
        var spawnPlace = this.pearlSpawn.transform.position;
        GameObject newGameObject = Instantiate(this.prefab, spawnPlace, Quaternion.identity);
        return newGameObject;
    }

    public GameObject Build(PearlSpec newSpec)
    {
        var pearl = Make();

        // Set the specs of the newly instantiated newGameObject
        // to the specs specified within newSpec.
        var pearlMotion = pearl.GetComponent<PearlMotion>();

        // Multiply the Force by the direction it should go (ie. where the mouse is pointing).
        pearlMotion.VelocityX =  newSpec.Force * newSpec.Direction.x;
        pearlMotion.VelocityY =  newSpec.Force * newSpec.Direction.y;
        return pearl;
    }
    public GameObject GenerateRandomPearl()
    {
        PearlSpec newSpec = new PearlSpec();
        return Build(newSpec);
    }
}
