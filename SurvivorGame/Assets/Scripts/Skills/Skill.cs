using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Skill", menuName ="Skill")]
public class Skill : ScriptableObject
{
    public enum SkillType {Projectile,Melee,Meteor }
    public SkillType skillType;
    public string skillName;
    public int damage;
    public int increaseAmount;
    [HideInInspector]public float lastActivationTime = -Mathf.Infinity;


    private void OnEnable()
    {
        ResetCooldown();
    }

    private void ResetCooldown()
    {
        lastActivationTime = -Mathf.Infinity;
    }

    public virtual void Activate(Transform spawnPos)
    {

    }

    public virtual void Upgrade()
    {

    }

    public void AddController(SkillType skillType,GameObject instance)
    {
        switch (skillType)
        {
            case SkillType.Projectile:
                instance.AddComponent<ProjectileController>();
                break;
            case SkillType.Melee:
                instance.AddComponent<MeleeController>();
                break;
            case SkillType.Meteor:
                instance.AddComponent<MeteorController>();
                break;
        }
    }

}
