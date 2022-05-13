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

        // Set the specs of the newly instantiated newGameObject
        // to the specs specified within newSpec.
        var projectileController = projectile.GetComponent<ProjectileController>();
        return projectile;
    }
    public GameObject GenerateRandomProjectile()
    {
        ProjectileSpec newSpec = new ProjectileSpec();
        return Build(newSpec);
    }
}
