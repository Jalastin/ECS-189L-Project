using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : Factory
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject projectileSpawn;
    public GameObject Make()
    {
        var spawnPlace = this.projectileSpawn.transform.position;
        GameObject newGameObject = Instantiate(this.prefab, spawnPlace, Quaternion.identity);
        return newGameObject;
    }

    public GameObject Build(ProjectileSpec newSpec)
    {
        var projectile = Make();

        // Get the vector between the projectileSpawn and the mouse.
        var mouseDiff = GameObject.Find("Main Camera").GetComponent<CameraController>().MousePosition - projectileSpawn.transform.position;
        var mouseDistance = mouseDiff.magnitude;
        var mouseDirection = mouseDiff / mouseDistance; 

        // Set the specs of the newly instantiated newGameObject
        // to the specs specified within newSpec.
        var projectileMotion = projectile.GetComponent<ProjectileMotion>();
        // Multiply the Force by the direction it should go (ie. where the mouse is pointing).
        projectileMotion.Velocity = new Vector2(newSpec.Force * mouseDirection.x, newSpec.Force * mouseDirection.y);
        return projectile;
    }
    public GameObject GenerateRandomProjectile()
    {
        ProjectileSpec newSpec = new ProjectileSpec();
        return Build(newSpec);
    }
}
