using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileBehaviour : SkillBehaviour
{
    private int damage;
    private bool canMove = false;
    private float searchRadius = 15f;
    private float projectileSpeed = 10f;
    private GameObject targetEnemy = null;

    public override void Init(Skill _skill, SkillProperty _skillProperty)
    {
        base.Init(_skill, _skillProperty);
        damage = skillProperty.damage;
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.2f).OnComplete(delegate
        {
            canMove = true;
            FindNearestEnemy();
        });
        
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        if (targetEnemy != null && targetEnemy.activeInHierarchy)
        {
            
            Vector3 directionToTarget = targetEnemy.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * projectileSpeed);
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, projectileSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
