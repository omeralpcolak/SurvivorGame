using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator enemyAnim;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Tween scaleTween;
    private Health enemyHealth;
    [SerializeField]private Transform effectPos;
    
    [HideInInspector]public AttackWaveGroup ownerAttackWaveGroup;
    public GameObject xp, coin, hitEffect,deathEffect;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        enemyHealth = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.localScale = Vector3.zero;
        scaleTween = transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        enemyHealth.OnHealthZero += EnemyDeath;
        enemyHealth.OnTakeDamage += EnemyTakingDamage;
    }

    void FixedUpdate()
    {
        if (!gameObject)
        {
            return;
        }

        navMeshAgent.destination = player.position;

        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= 4)
        {
            enemyAnim.SetBool("Attack", true);
        }
        else
        {
            enemyAnim.SetBool("Attack", false);
        }

    }


    private void EnemyTakingDamage()
    {
        Instantiate(hitEffect, transform);
    }
    

    private void EnemyDeath()
    {
        scaleTween.Kill();
        Instantiate(xp, effectPos.position, Quaternion.identity);
        Instantiate(coin, effectPos.position, Quaternion.identity);
        Instantiate(deathEffect, effectPos.position, Quaternion.identity);
        Destroy(gameObject);
        ownerAttackWaveGroup.Killed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(10);
        }
    }
}
