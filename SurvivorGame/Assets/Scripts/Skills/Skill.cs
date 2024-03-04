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
    public int damage;
    public float cooldownDuration;
    public Vector3 skillScale;
    public float rotateTime;
    public int level = 1;
}

[CreateAssetMenu(fileName ="New Skill", menuName ="Skill/BaseSkill")]
public class Skill : ScriptableObject
{
    public SkillProperty skillProperty;
    public SkillBehaviour skillPrefab;
    
    public bool isSingleUse;
    
    public int level;
    public int SkillLevel
    {
        get => PlayerPrefs.GetInt(name + "_SkillLevel", 0);
        set => PlayerPrefs.SetInt(name + "_SkillLevel", value);
    }

    [HideInInspector] public SkillBehaviour skillIns;

    [HideInInspector] public bool isOwned;
    [HideInInspector] public bool canBeUpgraded;
    [HideInInspector] public bool isUsed = false;
    [HideInInspector] public bool isCooldown = false;

    [HideInInspector]public int damage;
    [HideInInspector]public Vector3 skillScale;
    [HideInInspector]public float rotateTime;

    public void Activate(Transform spawnPos, MonoBehaviour monoBehaviour)
    {
        if (!isEligibleForActivation()) return;

        ExecuteSkill(spawnPos);
        HandleCooldown(monoBehaviour);
    }

    public void Upgrade()
    {
        if (level <= 5)
        {
            skillIns.Upgrade();
            level++;
            
        }
        int nextLevel = level + 1;

        if(nextLevel > 5)
        {
            canBeUpgraded = false;
        }
        
    }

    private bool isEligibleForActivation()
    {
        if (isSingleUse && isUsed) return false;
        if (isCooldown) return false;
        return true;
    }

    private void ExecuteSkill(Transform spawnPos)
    {
        skillIns = Instantiate(skillPrefab, spawnPos.position, Quaternion.identity, spawnPos);
        skillIns.Init(this, skillProperty);
        if (isSingleUse) isUsed = true;
    }

    private void HandleCooldown(MonoBehaviour monoBehaviour)
    {
        if (!isSingleUse)
        {
            isCooldown = true;
            monoBehaviour.StartCoroutine(CooldownCoroutine(skillProperty.cooldownDuration));
        }
    }

    private IEnumerator CooldownCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isCooldown = false;
    }


    private void OnEnable()
    {
        ResetState();
        damage = skillProperty.damage;
        skillScale = skillProperty.skillScale;
        rotateTime = skillProperty.rotateTime;
        level = skillProperty.level;
    }

    public void ResetState()
    {
        isUsed = false;
        isCooldown = false;
        isOwned = false;
        canBeUpgraded = true;

        if (skillIns)
        {
            Destroy(skillIns.gameObject);
        }
        else
        {
            return;
        }
    }
}
