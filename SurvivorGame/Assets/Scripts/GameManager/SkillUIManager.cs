using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUIManager : MonoBehaviour
{
    
    public Image skillIcon;
    public TMP_Text damageText;
    public TMP_Text cooldownText;
    public TMP_Text scaleText;
    public TMP_Text skillNameTxt;
    public GameObject cooldownPanel;
    public GameObject scalePanel;
    public GameObject skillInfoPanel;



    public Skill currentSkill;

    private void Start()
    {
        SetSkill(currentSkill);
    }

    public void SetSkill(Skill skill)
    {
        skill = currentSkill;
        skillIcon.sprite = skill.skillIcon;
        skillNameTxt.text = skill.skillName;
        damageText.text = "Damage: " + skill.damage.ToString();

        switch (skill.skillType)
        {
            case Skill.SkillType.Projectile:
                Projectile projectile = skill as Projectile;
                UpdateProjectileUI(projectile);
                break;
            case Skill.SkillType.Melee:
                Melee melee = skill as Melee;
                UpdateMeleeUI(melee);
                break;
            case Skill.SkillType.Meteor:
                Meteor meteor = skill as Meteor;
                UpdateMeteorUI(meteor);
                break;
        }
    }

    public void UpgradeSkill()
    {
        currentSkill.Upgrade();
        SetSkill(currentSkill);
        
    }

    private void UpdateProjectileUI(Projectile projectile)
    {
        cooldownPanel.SetActive(true);
        scalePanel.SetActive(false);
        cooldownText.text = "Cooldown: " + projectile.cooldownDuration.ToString() + "s";
    }

    private void UpdateMeleeUI(Melee melee)
    {
        cooldownPanel.SetActive(false);
        scalePanel.SetActive(true);
        scaleText.text = "Scale: " + melee.maxScale.x.ToString();
    }

    private void UpdateMeteorUI(Meteor meteor)
    {
        cooldownPanel.SetActive(true);
        scalePanel.SetActive(true);
        cooldownText.text = "Cooldown: " + meteor.cooldownDuration.ToString() + "s";
        scaleText.text = "Scale: " + meteor.maxScale.x.ToString();
    }
}
