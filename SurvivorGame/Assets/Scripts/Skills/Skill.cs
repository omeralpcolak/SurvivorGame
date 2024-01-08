using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class SkillProperty
{
    public string name;
    public Sprite icon;
    public float defaultAmount;
    public int damage;
    public float levelMultiplier;
    public float cooldownDuration;

    public float GetAmount(int level)
    {
        return defaultAmount + (levelMultiplier * levelMultiplier);
    }
}

[CreateAssetMenu(fileName ="New Skill", menuName ="Skill/BaseSkill")]
public class Skill : ScriptableObject
{
    public SkillProperty skillProperty;
    public SkillBehaviour skillPrefab;
    [HideInInspector]public float cooldownTime;

    public virtual void Activate(Transform spawnPos)
    {
        Instantiate(skillPrefab, spawnPos.position, Quaternion.identity).Init(this,skillProperty);
        
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
        cooldownTime = 0;
    }
}
