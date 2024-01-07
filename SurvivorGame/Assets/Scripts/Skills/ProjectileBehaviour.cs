using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : SkillBehaviour
{
    private int damage;

    public override void Init(Skill _skill, SkillProperty _skillProperty)
    {
        base.Init(_skill, _skillProperty);
        damage = skillProperty.damage;
    }
}
