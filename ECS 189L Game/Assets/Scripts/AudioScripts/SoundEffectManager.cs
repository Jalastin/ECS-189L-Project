using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] AudioSource ProjectileCollision;
    [SerializeField] AudioSource ChargingThrow;
    [SerializeField] AudioSource ProjectileRelease;
    [SerializeField] AudioSource RollingProjectile;
    [SerializeField] AudioSource Teleportation;

    public void PlayProjectileCollisionSound()
    {
        ProjectileCollision.Play();
    }

    public void PlayChargingThrowSound()
    {
        ChargingThrow.Play();
    }

    public void PlayProjectileReleaseSound()
    {
        ProjectileRelease.Play();
    }

    public void PlayRollingProjectileSound()
    {
        RollingProjectile.Play();
    }

    public void PlayTeleportationSound()
    {
        Teleportation.Play();
    }
}
