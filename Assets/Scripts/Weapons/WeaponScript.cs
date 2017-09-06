using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour {
    
    protected GameObject bulletSpawner;
    

    /* Recoil:
     * Recoil begins with the initial. After each shot,
     * it increases by stepRecoil, to a maximum of
     * maximumRecoil. 
     * After the timeToDecreaseRecoil, it decreases by
     * stepRecoil.
     */

    public bool hasRecoil;

    // Initial recoil force.
    public float mininumRecoil; 

    // Maximum recoil force possible.
    public float maximumRecoil;

    // Current recoil, which shall change.
    private float currentRecoil;

    // How much does the recoil increase.
    public float stepRecoil;

    // Time to decrease recoil by stepRecoil.
    public int timeToDecreaseRecoil;

    /* Fire rate */
    private bool canFire = true; // Can fire both primary and secondary
    private bool canFirePrimary = true;
    private bool canFireSecondary = true;

    public float primaryFireRate;
    public float secondaryFireRate;

    public bool primaryAndSecondaryShareCooldown = false;

    // Use this for initialization
    void Start()
    {
        if(transform.childCount != 0)
            bulletSpawner = transform.GetChild(0).gameObject;
    }

    void Update()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(bulletSpawner != null)
            Gizmos.DrawLine(bulletSpawner.transform.position, bulletSpawner.transform.position + (100 * transform.right));
    }

    public void PrimaryAttack()
    {   
        StartCoroutine(PrimaryAttackCoroutine());
    }

    public void SecondaryAttack()
    {
        StartCoroutine(SecondaryAttackCoroutine());
    }

    private IEnumerator PrimaryAttackCoroutine()
    {
        if (bulletSpawner != null && canFirePrimary && canFire)
        {
            if (primaryAndSecondaryShareCooldown)
            {
                canFire = false;
            }
            canFirePrimary = false;
            PrimaryAttackImplementation();
            /*GameObject bullet = Instantiate(prefab) as GameObject;
            bullet.transform.position = bulletSpawner.transform.position;
            bullet.transform.rotation = bulletSpawner.transform.rotation;*/
            yield return new WaitForSeconds(primaryFireRate);
            if (primaryAndSecondaryShareCooldown)
            {
                canFire = true;
            }
            canFirePrimary = true;    
        }
    }

    private IEnumerator SecondaryAttackCoroutine()
    {
        if (bulletSpawner != null && canFireSecondary && canFire)
        {
            if(primaryAndSecondaryShareCooldown)
            {
                canFire = false;
            }
            canFireSecondary = false;
            SecondaryAttackImplementation();
            yield return new WaitForSeconds(secondaryFireRate);
            if (primaryAndSecondaryShareCooldown)
            {
                canFire = true;
            }
            canFireSecondary = true;
        }
    }

    abstract public void PrimaryAttackImplementation();
    abstract public void SecondaryAttackImplementation();
}
