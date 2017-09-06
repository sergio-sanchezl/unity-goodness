using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleScript : WeaponScript {
    
    public float damage;
    public float bulletForce;
    public UnityEngine.Object bulletPrefab;

    public override void PrimaryAttackImplementation()
    {
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.position = base.bulletSpawner.transform.position;
            bullet.transform.rotation = base.bulletSpawner.transform.rotation;
            bullet.GetComponent<BulletScript>().SetParameters(base.bulletSpawner.transform.position,damage,bulletForce);
        }
        else
        {
            Debug.LogError("Forgot to put the bullet into the prefab slot...");
        }
    }

    public override void SecondaryAttackImplementation()
    {
        // do nothing. this weapon has no secondary attack.
    }
}
