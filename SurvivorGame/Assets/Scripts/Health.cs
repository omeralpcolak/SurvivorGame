using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Health : MonoBehaviour, IHealth
{
    public int maxHealth;
    private int currentHealth;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public Action OnHealthZero;
    public Action OnTakeDamage;

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            OnHealthZero();
        }
        else
        {
            OnTakeDamage();
        }

    }



    /*public int health;
    private int currentHealth;
    public GameObject hitEffect;
    public Transform hitEffectPos;
    public EntityType entityType;
    
    [SerializeField] private Healthbar healthbar;

    //Interface and event register and system actions.
   

    private void Start()
    {
        currentHealth = health;
        healthbar.UpdateHealthBar(health, currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.UpdateHealthBar(health, currentHealth);
        Instantiate(hitEffect, hitEffectPos.position, Quaternion.identity,transform);
        CameraShake.instance.Shake(0.4f, 2f);

        if (currentHealth<= 0)
        {

            switch (entityType)
            {
                case EntityType.Player:
                    PlayerController playerController = GetComponent<PlayerController>();
                    playerController.PlayerDeath();
                    break;
                case EntityType.Enemy:
                    EnemyController enemyController = GetComponent<EnemyController>();
                    enemyController.EnemyDeath();
                    break;
            }

            
        }
    }*/
    
}
