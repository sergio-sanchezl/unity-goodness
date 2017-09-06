using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {

    public Object explosionEffect;
    public float explosionRadius;
    public float explosionMaxStrength;
   
    public float seconds = 2.0f;

    public float force = 20F;
    private Rigidbody rb;

    public float maxDamage;

    private bool exploded = false;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(TimedExplosion(seconds));
        rb.AddRelativeForce(new Vector3(0F, force, 0),ForceMode.Impulse);
	}
	
    public void SetParameters(float explosionMaxStrength, float explosionRadius, float maxDamage, float launchForce)
    {
        this.explosionMaxStrength = explosionMaxStrength;
        this.explosionRadius = explosionRadius;
        this.maxDamage = maxDamage;
        this.force = launchForce;
    }
    IEnumerator TimedExplosion(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Explode();
    }

    void Explode()
    {
        Destroy(GetComponent<SphereCollider>());
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        float distance;
        float pushForce;
        float interpolant;
        foreach (Collider col in colliders)
        {

            distance = Vector3.Distance(col.transform.position, transform.position);
            interpolant = Mathf.Clamp01(Mathf.InverseLerp(0, explosionRadius, distance));
            //Debug.Log("Distance: " + distance + "\nInterpolant: " + interpolant);
            Rigidbody hitRigidbody = col.gameObject.GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                pushForce = Mathf.Lerp(explosionMaxStrength, 0, interpolant);
                //Debug.DrawLine(col.transform.position, transform.position, Color.cyan, 2f);
                //Debug.DrawRay(col.transform.position, Vector3.Normalize(col.transform.position - transform.position) * pushForce, Color.red, 2f);
                hitRigidbody.AddForce(Vector3.Normalize(col.transform.position - transform.position) * pushForce);

            }
            HealthScript healthScript = col.gameObject.GetComponent<HealthScript>();
            if (healthScript != null)
            {
                healthScript.Damage(Mathf.Lerp(maxDamage, 0, interpolant));
            }

        }

        if (!exploded)
        {
            GameObject effectGameObject = Instantiate(explosionEffect) as GameObject;
            effectGameObject.transform.localScale = new Vector3(explosionRadius * 2, explosionRadius * 2, explosionRadius * 2);
            effectGameObject.transform.position = this.transform.position;
            exploded = true;
        }
        Destroy(this.gameObject);
    }

	// Update is called once per frame
	void Update () {
	    // Nothing done here.	
	}
}
