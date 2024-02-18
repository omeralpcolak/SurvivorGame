using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public CharacterConfig config;

    protected Rigidbody rb;
    protected Health health;
    protected Animator anim;

    protected int movementSpeed;

    public void SetUpComponents(CharacterBase characterBase)
    {
        rb = characterBase.GetComponent<Rigidbody>();
        health = characterBase.GetComponent<Health>();
        anim = characterBase.GetComponent<Animator>();

        movementSpeed = characterBase.config.movementSpeed;

        health.SetUpHealth(characterBase.config.maxHealth);
        health.OnHealthZero += CharacterDeath;
        health.OnTakeDamage += CharacterTakingDamage;
    }

    public virtual void CharacterAttack()
    {

    }

    public virtual void CharacterMovement()
    {

    }

    public virtual void CharacterDeath()
    {

    }

    public virtual void CharacterTakingDamage()
    {

    }
}
