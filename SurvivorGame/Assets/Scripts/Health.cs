using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        CameraShake.instance.Shake(2f, 1f);
        if (health<= 0)
        {
            Destroy(gameObject);
        }
    }
}
