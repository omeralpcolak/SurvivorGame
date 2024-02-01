using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileBehaviour : SkillBehaviour
{
    private bool canMove = false;
    private bool isBeingDestroyed = false;
    private float searchRadius = 15f;
    [SerializeField]private float projectileSpeed;
    private GameObject targetEnemy = null;
    private Tween scaleTween;

    public override void Init(Skill _skill, SkillProperty _skillProperty)
    {
        base.Init(_skill, _skillProperty);
    }

    public override void Upgrade()
    {
        
    }

    private void Start()
    {
        transform.parent = null;
        transform.localScale = Vector3.zero;
        FindNearestEnemy();
        scaleTween = transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.2f).OnComplete(delegate
        {
            canMove = true;
        });
        
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        if (isBeingDestroyed)
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
            isBeingDestroyed = true;
            scaleTween = transform.DOScale(Vector3.zero, 0.2f).OnComplete(delegate
            {
                scaleTween.Kill(true);
                Destroy(gameObject);
            });
        
        }
    }

    private void KillTween()
    {
        if(scaleTween != null)
        {
            scaleTween.Kill();
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
                Vector3 directionToTarget = targetEnemy.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = targetRotation;
                minDistance = distToEnemy;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(skill.damage);
            KillTween();
            Destroy(gameObject);
        }
    }
}
