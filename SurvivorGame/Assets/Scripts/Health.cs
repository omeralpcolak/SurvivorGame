using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        CameraShake.instance.Shake(0.2f, 1.5f);
        if (health<= 0)
        {
            transform.DOScale(Vector3.zero, 0.2f).OnComplete(delegate
            {
                Destroy(gameObject);
            });
            
        }
    }
}
