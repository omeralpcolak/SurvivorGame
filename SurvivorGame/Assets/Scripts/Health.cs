using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public int health;
    public GameObject hitEffect;
    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(hitEffect, transform.position, Quaternion.identity,transform);
        CameraShake.instance.Shake(0.4f, 2f);
        if (health<= 0)
        {
            Destroy(gameObject);
        }
    }
}
