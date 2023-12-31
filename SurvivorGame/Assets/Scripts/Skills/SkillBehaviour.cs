using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviour : MonoBehaviour
{
    protected Skill skill;
    protected SkillProperty skillProperty;
    public int level;

    public virtual void Init(Skill _skill, SkillProperty _skillProperty)
    {
        skill = _skill;
        skillProperty = _skillProperty;
    }

    public virtual void Upgrade(int _level)
    {
        level = _level;
    }
}
