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
    private float projectileLastAttackTime;
    private float areaLastAttackTime;
    private float movementSpeed;
    private float attackSpeed;
    private float rotationSpeed = 25f;
    private bool canAttack = true;

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
        float currentTime = Time.time;

        foreach (Skill skill in skills)
        {
            switch (skill.skillType)
            {
                case Skill.SkillType.Projectile:
                    if (currentTime - skill.lastActivationTime >= attackSpeed * .1f)
                    {
                        skill.Activate(projectileSpawnPos);
                        skill.lastActivationTime = currentTime; // Update this skill's last activation time
                    }
                    break;

                case Skill.SkillType.Melee:
                    skill.Activate(meleeSpawnPos);
                    break;

                case Skill.SkillType.Area:
                    if (currentTime - areaLastAttackTime >= attackSpeed)
                    {
                        skill.Activate(transform);
                        areaLastAttackTime = currentTime;
                    }
                    break;
            }
        }
    }


}

