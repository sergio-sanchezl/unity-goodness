using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : HealthScript {
    
    public GameObject healthIndicator;

    private Text healthText;
    private RectTransform healthBar;

    public override void Damage(float damage)
    {
        float roundedDamage = Mathf.Round(damage);
        // If the damage gets the health below 0, then set it to 0 and die.
        currentHealth = (currentHealth - roundedDamage <= 0) ? 0 : currentHealth - roundedDamage;
        UpdateHealthView();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Heal(float healing)
    {
        float roundedHealing = Mathf.Round(healing);
        // If healing is above max health, limit the hp to maximum. if not, just set the
        // current health to be the sum of the current and the healing.
        currentHealth = (currentHealth + roundedHealing >= maxHealth) ? maxHealth : currentHealth + roundedHealing;
        UpdateHealthView();
    }

    public void Die()
    {
        Debug.Log("died. :(");
    }

    // Use this for initialization
    void Start () {
        base.InitializeHealth(maxHealth);
        healthBar = healthIndicator.transform.GetChild(1).GetComponent<RectTransform>();
        healthText = healthIndicator.transform.GetChild(2).GetComponent<Text>();
        UpdateHealthView();
    }

    void UpdateHealthView()
    {
        float ratio = currentHealth / maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;
        healthBar.localScale = new Vector3(ratio,1f,1f);
    }
}
