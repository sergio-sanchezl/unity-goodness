using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectScript : HealthScript {

    public Object prefabToSpawnWhenBroken;

    public override void Damage(float damage)
    {
        // If the damage gets the health below 0, then set it to 0 and die.
        currentHealth = (currentHealth - damage <= 0) ? 0 : currentHealth - damage;
        if (currentHealth <= 0)
        {
            BreakObject();
        }
    }

    public override void Heal(float healing)
    {
        // If healing is above max health, limit the hp to maximum. if not, just set the
        // current health to be the sum of the current and the healing.
        currentHealth = (currentHealth + healing >= maxHealth) ? maxHealth : currentHealth + healing;
    }

    public void BreakObject()
    {
        Debug.Log("a box got broken. ayyy");
        if(prefabToSpawnWhenBroken != null)
        {
            GameObject instantiatedPrefab = Instantiate(prefabToSpawnWhenBroken) as GameObject;
            instantiatedPrefab.transform.position = this.gameObject.transform.position;
            instantiatedPrefab.transform.rotation = this.gameObject.transform.rotation;
            instantiatedPrefab.transform.localScale = this.gameObject.transform.localScale;

            Rigidbody rb;

            if((rb = instantiatedPrefab.GetComponent<Rigidbody>()) != null)
            {
                Rigidbody parentRb = this.gameObject.GetComponent<Rigidbody>();
                if (parentRb != null)
                {
                    rb.velocity = parentRb.velocity;
                }
            }
        }
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        base.InitializeHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
