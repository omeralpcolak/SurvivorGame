using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Transform effectPos;
    private Animator enemyAnim;
    [HideInInspector]public AttackWaveGroup ownerAttackWaveGroup;
    public GameObject xp, coin, deathEffect;
    Tween scaleTween;

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
        Debug.Log("Distance is " +distance);
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
        //Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
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
            other.GetComponent<Health>().TakeDamage(10);
        }
    }
}
