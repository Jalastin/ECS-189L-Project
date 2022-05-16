using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMotion))]
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float teleportDelay = 0.3f;
    private float timeElapsed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contact!");
        this.GetComponent<ProjectileMotion>().HasCollided = true;
    }

    void Start()
    {
        this.timeElapsed = 0;
    }

    void Update()
    {
        this.GetComponent<ProjectileMotion>().Fire();
        if (this.GetComponent<ProjectileMotion>().HasCollided)
        {
            if (this.timeElapsed >= this.teleportDelay)
            {
                this.GetComponent<ProjectileMotion>().HasCollided = false;
                this.timeElapsed = 0;
                Debug.Log(this.transform.position);
                Destroy(this.gameObject);
            }
            this.timeElapsed += Time.deltaTime;
        }
    }
}
