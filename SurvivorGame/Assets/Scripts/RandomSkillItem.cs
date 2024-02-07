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
    public Image star;
    public Transform levelStars;

    public void Init(RandomSkillPanel _owner,Skill skill,bool isItUpgrade)
    {
        this.skill = skill;
        this.isItUpgrade = isItUpgrade;
        owner = _owner;
        GetComponent<Image>().sprite = skill.skillProperty.icon;
        GetComponentInChildren<TMP_Text>().text = skill.skillProperty.name;

        if (isItUpgrade == true)
        {
            Debug.Log(isItUpgrade);
            Debug.Log("func is working");
            for(int i = 1; i<this.skill.level+1; i++)
            {
                Debug.Log(this.skill.level);
                Debug.Log(i);
                Instantiate(star, levelStars);
            }
        }
    }

    public void OnClick()
    {
        if (!isItUpgrade)
        {
            skill.isOwned = true;
            GameSessionManager.instance.AddSkill(skill);
        }
        else
        {
            skill.Upgrade();
        }
        owner.Hide();
    }
}
