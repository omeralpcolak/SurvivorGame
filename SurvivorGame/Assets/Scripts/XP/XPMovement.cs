using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPMovement : MonoBehaviour
{
    private GameObject player;
    private float xpMovementSpeed = 2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;
        transform.Translate(direction * Time.deltaTime * xpMovementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
