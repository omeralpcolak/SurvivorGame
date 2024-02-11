using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EntityType
{
    Player,
    Enemy
}

public class Health : MonoBehaviour
{
    public int health;
    public GameObject hitEffect;
    public GameObject deathEffect;
    public bool isObjectEnemy;
    public GameObject xp;
    public GameObject coin;
    public EntityType entityType;
    private int currentHealth;
    [SerializeField] private Healthbar healthbar;

   

    private void Start()
    {
        currentHealth = health;
        healthbar.UpdateHealthBar(health, currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.UpdateHealthBar(health, currentHealth);
        Instantiate(hitEffect, transform.position, Quaternion.identity,transform);
        CameraShake.instance.Shake(0.4f, 2f);

        if (currentHealth<= 0)
        {

            switch (entityType)
            {
                case EntityType.Player:
                    //player taking damage
                    break;
                case EntityType.Enemy:
                    Instantiate(xp, transform.position, Quaternion.identity);
                    Instantiate(coin, transform.position, Quaternion.identity);
                    break;
            }

            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
