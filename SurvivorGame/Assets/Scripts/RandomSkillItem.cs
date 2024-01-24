using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomSkillItem : MonoBehaviour
{
    private RandomSkillPanel owner;
    private Skill skill;
    private bool isItUpgrade;

    public void Init(RandomSkillPanel _owner,Skill skill,bool isItUpgrade)
    {
        this.skill = skill;
        this.isItUpgrade = isItUpgrade;
        owner = _owner;
        GetComponent<Image>().sprite = skill.skillProperty.icon;
        GetComponentInChildren<TMP_Text>().text = skill.skillProperty.name;
    }

    public void OnClick()
    {
        owner.Hide();
        if (!isItUpgrade)
        {
            GameSessionManager.instance.AddSkill(skill);
            Debug.Log("skill is added");
        }
        else
        {
            skill.Upgrade();
            Debug.Log("skill is upgraded!");
        }
    }
}
