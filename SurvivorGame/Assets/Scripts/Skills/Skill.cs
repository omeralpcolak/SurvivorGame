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
}

[CreateAssetMenu(fileName ="New Skill", menuName ="Skill/BaseSkill")]
public class Skill : ScriptableObject
{
    public SkillProperty skillProperty;
    public SkillBehaviour skillPrefab;
    public bool isSingleUse;
    [HideInInspector] public bool isUsed = false;
    [HideInInspector] public bool isCooldown = false;

    public void Activate(Transform spawnPos, MonoBehaviour monoBehaviour)
    {
        if (!isEligibleForActivation()) return;

        ExecuteSkill(spawnPos);
        HandleCooldown(monoBehaviour);
    }

    private bool isEligibleForActivation()
    {
        if (isSingleUse && isUsed) return false;
        if (isCooldown) return false;
        return true;
    }

    private void ExecuteSkill(Transform spawnPos)
    {
        Instantiate(skillPrefab, spawnPos.position, Quaternion.identity, spawnPos).Init(this, skillProperty);
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
    }

    private void ResetState()
    {
        isUsed = false;
        isCooldown = false;
    }
}
