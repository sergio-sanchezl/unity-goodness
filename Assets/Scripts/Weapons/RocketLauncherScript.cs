using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherScript : WeaponScript {

    public float rocketMaxStrength = 100f;
    public float rocketRadius = 20f;
    public float damage;
    public float rocketSpeed;
    public UnityEngine.Object rocketPrefab;

    public override void PrimaryAttackImplementation()
    {
        if(rocketPrefab != null)
        {
            GameObject rocket = Instantiate(rocketPrefab) as GameObject;
            rocket.transform.position = base.bulletSpawner.transform.position;
            rocket.transform.rotation = base.bulletSpawner.transform.rotation;
            rocket.GetComponent<RocketScript>().SetParameters(rocketMaxStrength, rocketRadius, damage, rocketSpeed);
        } else
        {
            Debug.LogError("Forgot to put the rocket into the prefab slot...");
        }
    }

    public override void SecondaryAttackImplementation()
    {
        // do nothing. this weapon has no secondary attack.
    }
}
