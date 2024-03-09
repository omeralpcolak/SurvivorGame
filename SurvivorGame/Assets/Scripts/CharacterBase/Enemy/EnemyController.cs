using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyController : CharacterBase
{
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Tween scaleTween;
    [SerializeField]private Transform effectPos;
    [HideInInspector]public AttackWaveGroup ownerAttackWaveGroup;
    public GameObject xp, coin,deathEffect;

    void Start()
    {
        SetUpComponents(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameSessionManager.instance.EnemySelfDestory += SelfDestroy;
        transform.localScale = Vector3.zero;
        scaleTween = transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
    }

    void FixedUpdate()
    {
        if (!gameObject || !GameSessionManager.instance.gameStart)
        {
            return;
        }

        navMeshAgent.destination = player.position;

        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= 4)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

    }


    public override void CharacterTakingDamage()
    {
        base.CharacterTakingDamage();
    }


    public override void CharacterDeath()
    {
        scaleTween.Kill();
        Instantiate(xp, effectPos.position, Quaternion.identity);
        Instantiate(coin, effectPos.position, Quaternion.identity);
        Instantiate(deathEffect, effectPos.position, Quaternion.identity);
        Destroy(gameObject);
        ownerAttackWaveGroup.Killed();
        Level.instance.CheckLevelComplete();
        
    }


    public void SelfDestroy()
    {
        Instantiate(deathEffect, effectPos.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(10);
        }
    }
}
