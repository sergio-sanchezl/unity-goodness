using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public LayerMask lm;

    bool triedToDealDamageYet = false;
    public Vector3 initialPoint;
    LineRenderer lr;
    float damage;
    float pushForce;

    public Transform ini;
    public Transform fin;

    // Use this for initialization
    void Start () {
        lr = this.gameObject.GetComponent<LineRenderer>();
        lr.numPositions = 2;
	}

    public void SetParameters(Vector3 initialPoint, float damage, float pushForce)
    {
        this.initialPoint = initialPoint;
        this.damage = damage;
        this.pushForce = pushForce;
    }

	// Update is called once per frame
	void Update () {
		if(!triedToDealDamageYet)
        {
            triedToDealDamageYet = true;
            Shoot();
        }
	}

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(initialPoint,transform.right, out hit, 100, lm))
        {
            lr.SetPosition(0, initialPoint);
            ini.transform.position = initialPoint;
            lr.SetPosition(1, hit.point);
            fin.transform.position = hit.point;
            GameObject hitGameObject = hit.transform.gameObject;
            Rigidbody rb;
            if((rb = hitGameObject.GetComponent<Rigidbody>()) != null)
            {
                rb.AddForce(Vector3.Normalize(hit.point - initialPoint) * pushForce, ForceMode.Impulse);
            }
            HealthScript healthScript;
            if((healthScript = hitGameObject.GetComponent<HealthScript>()) != null)
            {
                healthScript.Damage(damage);
            }

            Debug.Log("This bullet has hit: " + hit.transform.gameObject.name);
        } else
        {
            lr.SetPosition(0, initialPoint);
            ini.transform.position = initialPoint;
            lr.SetPosition(1, initialPoint + (transform.right * 100));
            fin.transform.position = initialPoint + (transform.right * 100);
        }
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
