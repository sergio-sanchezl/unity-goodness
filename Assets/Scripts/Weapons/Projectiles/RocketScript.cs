using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

    public float rocketMaxStrength = 100f;
    public float rocketRadius = 20f;

    public Object explosionEffect;

    Rigidbody rb;

    private Collider myCollider;

    public float maxDamage = 10;

    public GameObject smokeParticleEmitter;

    private bool exploded = false;

    public float speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        StartCoroutine(ScheduleDestroy(5));
	}

    public void SetParameters(float rocketMaxStrength, float rocketRadius, float damage, float speed)
    {
        this.rocketMaxStrength = rocketMaxStrength;
        this.rocketRadius = rocketRadius;
        this.maxDamage = damage;
        this.speed = speed;
    }
	
    IEnumerator ScheduleDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(ScheduleRocketDestroy(0));
    }
    IEnumerator ScheduleRocketDestroy(float seconds)
    {
        smokeParticleEmitter.transform.parent = null;
        smokeParticleEmitter.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
        StartCoroutine(ScheduleSmokeDestroy(3));

    }

    IEnumerator ScheduleSmokeDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(smokeParticleEmitter);
    }

    // Update is called once per frame
    void FixedUpdate () {
        rb.AddRelativeForce(new Vector3(speed, 0f, 0f));
	}

    void Explode()
    {
        Destroy(GetComponent<SphereCollider>());
        Collider[] colliders = Physics.OverlapSphere(transform.position, rocketRadius);

        float distance;
        float pushForce;
        float interpolant;
        foreach (Collider col in colliders)
        {

            distance = Vector3.Distance(col.transform.position, transform.position);
            interpolant = Mathf.Clamp01(Mathf.InverseLerp(0, rocketRadius, distance));
            //Debug.Log("Distance: " + distance + "\nInterpolant: " + interpolant);
            Rigidbody hitRigidbody = col.gameObject.GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                pushForce = Mathf.Lerp(rocketMaxStrength, 0, interpolant);
               // Debug.DrawLine(col.transform.position, transform.position, Color.cyan, 2f);
               // Debug.DrawRay(col.transform.position, Vector3.Normalize(col.transform.position - transform.position) * pushForce, Color.red, 2f);
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
            effectGameObject.transform.localScale = new Vector3(rocketRadius * 2, rocketRadius * 2, rocketRadius * 2);
            effectGameObject.transform.position = this.transform.position;
            exploded = true;
        }


        StartCoroutine(ScheduleDestroy(0));

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Projectile")
        {
            //Debug.Log("Collided with tag: " + collision.transform.tag);
            Explode();

        }
        else
        {
            Physics.IgnoreCollision(collision.collider, myCollider);
        }
    }
}
