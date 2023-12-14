using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerConfig playerConfig;
    public FloatingJoystick joystick;
    public List<PlayerAttacks> playerAttacks;

    private Vector3 moveVector;
    private float lastAttackTime;
    private float movementSpeed;
    private float attackSpeed;
    private float rotationSpeed = 25f;
    private bool canAttack;

    private Rigidbody playerRb;
    private Animator playerAnim;


    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        movementSpeed = playerConfig.movementSpeed;
        attackSpeed = playerConfig.attackSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
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
            canAttack = true;
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            playerAnim.SetBool("running", false);
            canAttack = false;
        }

        playerRb.MovePosition(playerRb.position + moveVector);
    }

    private void PlayerAttack()
    {
        if(Time.time - lastAttackTime >= attackSpeed && canAttack)
        {
            foreach(PlayerAttacks playerAttack in playerAttacks)
            {
                playerAttack.Attack();
                lastAttackTime = Time.time;
            }
        }
    }
}
