using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileController : MonoBehaviour
{
    bool canMove = false;
    private int projectileDamage;
    private float projectileSpeed;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.1f).OnComplete(delegate
        {
            canMove = true;
            transform.parent = null;
        });
    }

    public void ProjectileUpgrade(int newDamage, float newSpeed)
    {
        projectileDamage = newDamage;
        projectileSpeed = newSpeed;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(projectileDamage);
            transform.DOScale(0, 0.1f).OnComplete(delegate
            {
                Destroy(gameObject);
            });


        }
    }


}
