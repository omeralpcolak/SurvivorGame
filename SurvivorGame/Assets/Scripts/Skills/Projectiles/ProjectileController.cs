using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileController : MonoBehaviour
{
    private GameObject targetEnemy = null;
    private float searchRadius = 10f;
    private int projectileDamage;
    private float projectileSpeed;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.1f).OnComplete(delegate
        {
            transform.parent = null;
            //FindNearestEnemy();
        });
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        targetEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distToEnemy = Vector3.Distance(enemy.transform.position, currentPos);
            if (distToEnemy < minDistance && distToEnemy <= searchRadius)
            {
                targetEnemy = enemy;
                minDistance = distToEnemy;
            }
        }
    }

    public void ProjectileUpgrade(int newDamage, float newSpeed)
    {
        projectileDamage = newDamage;
        projectileSpeed = newSpeed;
    }

    void FixedUpdate()
    {
        FindNearestEnemy();
        if (targetEnemy != null && targetEnemy.activeInHierarchy)
        {
            Vector3 directionToTarget = targetEnemy.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * projectileSpeed);

            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, projectileSpeed * Time.deltaTime);
        }
        else
        {
            transform.DOScale(Vector3.zero, 0.1f).OnComplete(delegate
            {
                Destroy(gameObject);
            });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(projectileDamage);
            transform.DOScale(Vector3.zero, 0.1f).OnComplete(delegate
            {
                Destroy(gameObject);
            });
        }
    }
}
