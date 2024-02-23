using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : CharacterBase
{
    [HideInInspector]public FloatingJoystick joystick;
    public Transform spawnPos;
    public List<Skill> skills;

    private Vector3 moveVector;
    private float rotationSpeed = 25f;
    private bool canAttack;
    

    private void Start()
    {
        SetUpComponents(this);
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();
    }

    private void FixedUpdate()
    {
        if (!GameSessionManager.instance.gameStart)
        {
            return;
        }
        CharacterMovement();
        CharacterAttack();
    }

    public override void CharacterMovement()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * movementSpeed * Time.deltaTime;
        moveVector.z = joystick.Vertical * movementSpeed * Time.deltaTime;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(direction);

            anim.SetBool("running", true);
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            anim.SetBool("running", false);
        }

        rb.MovePosition(rb.position + moveVector);
    }

    public override void CharacterAttack()
    {
        foreach (Skill skill in skills)
        {
            skill.Activate(spawnPos, this);
        }
    }

    public override void CharacterTakingDamage()
    {
        
    }

    public override void CharacterDeath()
    {
        anim.SetTrigger("death");
        skills.Clear();
        GameSessionManager.instance.GameOver();
    }
}
