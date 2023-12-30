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
    public float cooldownDuration;
    [HideInInspector] public float cooldownTime;

    public virtual void Activate(Transform spawnPos)
    {

    }

    public virtual void Upgrade()
    {

    }

    private void OnEnable()
    {
        ResetCooldownTime();
    }

    private void ResetCooldownTime()
    {
        cooldownTime = 0f;
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
