using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public PlayerConfig playerConfig;
    [HideInInspector]public FloatingJoystick joystick;
    public Transform spawnPos;
    public List<Skill> skills;
    public Action TakeDamage;

    private Vector3 moveVector;
    private float movementSpeed;
    private float rotationSpeed = 25f;
    private bool canAttack;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private Health health;
    

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        health = GetComponent<Health>();
        movementSpeed = playerConfig.movementSpeed;
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();

        health.OnHealthZero += PlayerDeath;
        health.OnTakeDamage += PlayerTakingDamage;
    }

    private void FixedUpdate()
    {
        if (!GameSessionManager.instance.gameStart)
        {
            return;
        }
        Move();
        PlayerAttack();
    }

    private void Move()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * movementSpeed * Time.deltaTime;
        moveVector.z = joystick.Vertical * movementSpeed * Time.deltaTime;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(direction);

            playerAnim.SetBool("running", true);
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            playerAnim.SetBool("running", false);
        }

        playerRb.MovePosition(playerRb.position + moveVector);
    }

    private void PlayerAttack()
    {
        foreach (Skill skill in skills)
        {
            skill.Activate(spawnPos, this);
        }
    }

    private void PlayerTakingDamage()
    {
        Debug.Log("player take damage");
    }

    public void PlayerDeath()
    {
        playerAnim.SetTrigger("death");
    }
}
