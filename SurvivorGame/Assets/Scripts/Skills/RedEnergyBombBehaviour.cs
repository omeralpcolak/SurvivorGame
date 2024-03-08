using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnergyBombBehaviour : SkillBehaviour
{
    private SphereCollider sphereCollider;
    public ParticleSystem particle;

    public override void Init(Skill _skill, SkillProperty _skillProperty)
    {
        base.Init(_skill, _skillProperty);
    }

    public override void Upgrade()
    {
        skill.damage += 10;
        skill.skillScale *= 1.5f;
    }

    IEnumerator Start()
    {
        transform.parent = null;
        sphereCollider = GetComponent<SphereCollider>();
        yield return new WaitForSeconds(1.1f);
        Collider[] enemies = Physics.OverlapSphere(transform.position, sphereCollider.radius);

        Debug.Log(enemies);

        foreach (Collider enemy in enemies)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                enemy.GetComponent<Health>().TakeDamage(skill.damage);
            }
        }

        Destroy(gameObject);
    }

    
}
