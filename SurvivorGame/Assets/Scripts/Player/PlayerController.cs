using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerConfig playerConfig;
    public FloatingJoystick joystick;
    public List<Skill> skills;
    public Transform spawnPoint;

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
        
        if (canAttack)
        {
            PlayerAttack();
        }
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

            canAttack = true;
            playerAnim.SetBool("running", true);
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            canAttack = false;
            playerAnim.SetBool("running", false);
        }

        playerRb.MovePosition(playerRb.position + moveVector);
    }

    private void PlayerAttack()
    {
        if(Time.time - lastAttackTime >= attackSpeed*.1f)
        {
            foreach(Skill skill in skills)
            {
                skill.Activate(spawnPoint);
                lastAttackTime = Time.time;
            }
        }
    }
}
