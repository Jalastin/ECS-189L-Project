using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlFactory : Factory
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject pearlSpawn;
    public GameObject Make()
    {
        var spawnPlace = this.pearlSpawn.transform.position;
        GameObject newGameObject = Instantiate(this.prefab, spawnPlace, Quaternion.identity);
        return newGameObject;
    }

    public GameObject Build(PearlSpec newSpec)
    {
        var pearl = Make();

        // Get the vector between the pearlSpawn and the mouse.
        var mouseDiff = GameObject.Find("Main Camera").GetComponent<CameraController>().MousePosition - pearlSpawn.transform.position;
        var mouseDistance = mouseDiff.magnitude;
        var mouseDirection = mouseDiff / mouseDistance; 

        // Set the specs of the newly instantiated newGameObject
        // to the specs specified within newSpec.
        var pearlMotion = pearl.GetComponent<PearlMotion>();
        // Multiply the Force by the direction it should go (ie. where the mouse is pointing).
        pearlMotion.VelocityX =  newSpec.Force * mouseDirection.x;
        pearlMotion.VelocityY =  newSpec.Force * mouseDirection.y;
        return pearl;
    }
    public GameObject GenerateRandomPearl()
    {
        PearlSpec newSpec = new PearlSpec();
        return Build(newSpec);
    }
}
