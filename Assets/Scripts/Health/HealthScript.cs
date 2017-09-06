using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthScript : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;

    public void InitializeHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }
    public abstract void Damage(float damage);
    public abstract void Heal(float healing);
}

