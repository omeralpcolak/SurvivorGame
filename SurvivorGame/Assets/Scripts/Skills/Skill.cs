using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Skill", menuName ="Skill")]
public class Skill : ScriptableObject
{
    public enum SkillType {Projectile,Melee,Area }
    public SkillType skillType;
    public string skillName;
    public int damage;
    public int increaseAmount;


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
            case SkillType.Area:
                //instance.AddComponent<AreaController>();
                break;
        }
    }

}
