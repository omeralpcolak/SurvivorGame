using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerConfig playerConfig;
    public FloatingJoystick joystick;
    public Transform spawnPos;
    public List<Skill> skills;

    public Healthbar healthbar;

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
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();

        HealthSystem healthSystem = new HealthSystem(100);
        healthbar.Setup(healthSystem);
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

    public void PlayerDeath()
    {
        playerAnim.SetTrigger("death");
    }
}
