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
        Debug.Log(projectileDamage);
        Debug.Log(projectileSpeed);

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
}
