using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMotion))]
public class ProjectileController : MonoBehaviour
{

    void Start()
    {
        this.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    void Update()
    {
        this.GetComponent<ProjectileMotion>().Fire();
    }
}
