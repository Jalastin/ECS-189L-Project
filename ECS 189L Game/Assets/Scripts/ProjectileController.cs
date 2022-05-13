using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMotion))]
public class ProjectileController : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        this.GetComponent<ProjectileMotion>().Fire();
    }
}
