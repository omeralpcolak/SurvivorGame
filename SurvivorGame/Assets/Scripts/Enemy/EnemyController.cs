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
    [SerializeField]private Transform effectPos;
    
    [HideInInspector]public AttackWaveGroup ownerAttackWaveGroup;
    public GameObject xp, coin, deathEffect;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.localScale = Vector3.zero;
        scaleTween = transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
    }

    void FixedUpdate()
    {
        if (!gameObject)
        {
            return;
        }

        navMeshAgent.destination = player.position;

        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= 3)
        {
            enemyAnim.SetBool("Attack", true);
        }
        else
        {
            enemyAnim.SetBool("Attack", false);
        }

    }

    public void EnemyDeath()
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
            
        }
    }
}
