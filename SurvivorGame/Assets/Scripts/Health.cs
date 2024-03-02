using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Action OnHealthZero;
    public Action OnTakeDamage;
    public Healthbar healthbar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar();
    }

    public void SetUpHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }


    public float GetHealthPercent()
    {
        float percent = (float) currentHealth / maxHealth;
        return percent;
    }

    public void TakeDamage(int damageAmount)
    {
        OnTakeDamage();
        currentHealth -= damageAmount;
        healthbar.UpdateHealthBar();
        if (currentHealth <= 0)
        {
            OnHealthZero();
        }
    }

}
