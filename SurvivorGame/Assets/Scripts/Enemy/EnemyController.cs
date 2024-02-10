using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform player;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(10);
        }
    }
}
