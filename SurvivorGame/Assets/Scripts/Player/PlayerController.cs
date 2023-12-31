using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerConfig playerConfig;
    public FloatingJoystick joystick;
    public List<Skill> skills;
    public Transform projectileSpawnPos;
    public Transform meleeSpawnPos;

    private Vector3 moveVector;
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
        attackSpeed = playerConfig.attackSpeed; // Ensure this is updated elsewhere in your code as well
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameStart)
        {
            Move();
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
        
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
        float currentTime = Time.time;

        foreach(Skill skill in skills)
        {
            if(currentTime >= skill.cooldownTime)
            {
                switch (skill.skillType)
                {
                    case Skill.SkillType.Projectile:
                        skill.Activate(projectileSpawnPos);
                        skill.cooldownTime = currentTime + skill.cooldownDuration;
                        break;
                    case Skill.SkillType.Meteor:
                        skill.Activate(transform);
                        skill.cooldownTime = currentTime + skill.cooldownDuration;
                        break;
                    case Skill.SkillType.Melee:
                        skill.Activate(meleeSpawnPos);
                        break;
                }
            }
        }

        /*foreach (Skill skill in skills)
        {
            switch (skill.skillType)
            {
                case Skill.SkillType.Projectile:
                    if (currentTime - skill.lastActivationTime >= attackSpeed * 0.1)
                    {
                        skill.Activate(projectileSpawnPos);
                        skill.lastActivationTime = currentTime;
                    }
                    break;

                case Skill.SkillType.Melee:
                    skill.Activate(meleeSpawnPos);
                    break;

                case Skill.SkillType.Meteor:
                    if (currentTime - skill.lastActivationTime >= 4 / attackSpeed)
                    {
                        Debug.Log("Meteor activated at time: " + currentTime + " with attack speed: " + attackSpeed);
                        skill.Activate(transform);
                        skill.lastActivationTime = currentTime;
                    }
                    break;
            }
        }*/
    }
}
