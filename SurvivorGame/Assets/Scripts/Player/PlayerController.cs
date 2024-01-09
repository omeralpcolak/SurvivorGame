using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerConfig playerConfig;
    public FloatingJoystick joystick;
    public List<Skill> skills;
    public Transform spawnPos;

    private Vector3 moveVector;
    private float movementSpeed;
    private float rotationSpeed = 25f;
    private bool canAttack;

    private Rigidbody playerRb;
    private Animator playerAnim;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        movementSpeed = playerConfig.movementSpeed;
    }

    private void FixedUpdate()
    {
        Move();
        PlayerAttack();
    }

    private void Update()
    {
        

        
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
        float currentTime = Time.time;

        foreach (Skill skill in skills)
        {
            skill.Activate(spawnPos, this);
        }
    }
}
