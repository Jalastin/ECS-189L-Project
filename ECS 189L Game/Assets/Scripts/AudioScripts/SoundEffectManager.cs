using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] AudioSource ProjectileCollision;
    [SerializeField] AudioSource ProjectileRelease;
    [SerializeField] AudioSource Teleportation;

    public void PlayProjectileCollisionSound()
    {
        ProjectileCollision.Play();
    }

    public void PlayProjectileReleaseSound()
    {
        ProjectileRelease.Play();
    }

    public void PlayTeleportationSound()
    {
        Teleportation.Play();
    }
}
