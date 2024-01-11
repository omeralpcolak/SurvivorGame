using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public int health;
    public GameObject hitEffect;
    public GameObject deathEffect;
    public bool isObjectEnemy;
    public GameObject xp;
    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(hitEffect, transform.position, Quaternion.identity,transform);
        CameraShake.instance.Shake(0.4f, 2f);
        if (health<= 0)
        {
            if (isObjectEnemy)
            {
                Instantiate(xp, transform.position, Quaternion.identity);
            }
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
