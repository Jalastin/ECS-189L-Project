using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMotion))]
public class ProjectileController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        this.GetComponent<ProjectileMotion>().Fire();
    }
}
