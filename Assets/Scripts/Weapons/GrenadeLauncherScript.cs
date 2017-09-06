using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherScript : WeaponScript {

    public float explosionMaxStrength = 100f;
    public float explosionRadius = 20f;
    public float damage;
    public float launchForce;

    public UnityEngine.Object grenadePrefab;

    public override void PrimaryAttackImplementation()
    {
        if (grenadePrefab != null)
        {
            GameObject grenade = Instantiate(grenadePrefab) as GameObject;
            grenade.transform.position = base.bulletSpawner.transform.position;
            grenade.transform.rotation = base.bulletSpawner.transform.rotation;
            grenade.GetComponent<GrenadeScript>().SetParameters(explosionMaxStrength, explosionRadius, damage, launchForce);
        }
        else
        {
            Debug.LogError("Forgot to put the rocket into the prefab slot...");
        }
    }

    public override void SecondaryAttackImplementation()
    {
        // do nothing. this weapon has no secondary attack.
    }
}
