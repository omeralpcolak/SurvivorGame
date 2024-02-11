using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform player;

    public GameObject xp, coin, deathEffect;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }

    void FixedUpdate()
    {
        if (gameObject)
        {
            navMeshAgent.destination = player.position;
        }
        
    }

    public void EnemyDeath()
    {
        Instantiate(xp, transform.position, Quaternion.identity);
        Instantiate(coin, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(10);
        }
    }
}
