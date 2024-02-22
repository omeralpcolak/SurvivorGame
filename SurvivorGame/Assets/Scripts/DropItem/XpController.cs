using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpController : PickupItemController
{
    private Transform player;
    private float upwardsForce = 5f;
    private int speed = 30;
    private bool moveToThePlayer = false;

    public float xpAmount;
    Rigidbody rb;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(UpwardMove());
    }


    public override void Movement()
    {
        Vector3 direction = player.transform.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * speed); ;
    }

    public override void OnTriggeringWithThePlayer()
    {
        base.OnTriggeringWithThePlayer();
        Instantiate(itemEffect, player.transform);
        XPManager.instance.AddXp(xpAmount);
    }


    private void FixedUpdate()
    {
        if (!moveToThePlayer) return;

        Movement();
    }

    IEnumerator UpwardMove()
    {
        rb.AddForce(Vector3.up * upwardsForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        moveToThePlayer = true;
    }
}
