using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMotion))]
public class ProjectileController : MonoBehaviour
{
    // teleportDelay is how much time the pearl should roll
    // before the player gets teleported.
    [SerializeField] private float teleportDelay = 0.3f;
    // Counter to keep track of timeElapsed since initial contact with a platform.
    private float timeElapsed;

    private bool _hasCollided;
    public bool HasCollided
    {
        get => _hasCollided;
        set => _hasCollided = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Prevent the pearl from colliding with the Player itself.
        if (other.gameObject.tag != "Player")
        {
            Debug.Log("Collision!");
            this.HasCollided = true;
        }
    }

    void Start()
    {
        this.timeElapsed = 0;
        this.HasCollided = false;
    }

    void Update()
    {
        this.GetComponent<ProjectileMotion>().Fire();
        if (this.HasCollided)
        {
            if (this.timeElapsed >= this.teleportDelay)
            {
                this.HasCollided = false;
                this.timeElapsed = 0;
                // Debug.Log(this.transform.position);
                Destroy(this.gameObject);
            }
            this.timeElapsed += Time.deltaTime;
        }
    }
}
