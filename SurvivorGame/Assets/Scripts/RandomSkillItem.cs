using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RandomSkillItem : MonoBehaviour
{
    private RandomSkillPanel owner;
    private Skill skill;
    private bool isItUpgrade;
    public Image star;
    public Transform levelStars;
    public List<Image> allStars;

    public void Init(RandomSkillPanel _owner,Skill skill,bool isItUpgrade)
    {
        this.skill = skill;
        this.isItUpgrade = isItUpgrade;
        owner = _owner;
        GetComponent<Image>().sprite = skill.skillProperty.icon;
        GetComponentInChildren<TMP_Text>().text = skill.skillProperty.name;

        if (isItUpgrade == true)
        {
            //List<Image> allStars = new List<Image>();
            Debug.Log(isItUpgrade);
            Debug.Log("func is working");
            for(int i = 0; i<this.skill.level+1; i++)
            {
                Image starIns = Instantiate(star, levelStars);
                allStars.Add(starIns);
            }

            foreach(Image star in allStars)
            {
                CanvasGroup canvasGroup = star.GetComponent<CanvasGroup>();
                Debug.Log("foreach loop is working");
                StartCoroutine(FadingStars(canvasGroup));
            }

        }
    }

    IEnumerator FadingStars(CanvasGroup canvasGroup)
    {
        while (true)
        {
            Debug.Log("fading begin");
            canvasGroup.DOFade(0f, 1)
                .WaitForCompletion(true);

            yield return new WaitForSecondsRealtime(1);

            canvasGroup.DOFade(1f, 1)
                .WaitForCompletion(true);

            yield return new WaitForSecondsRealtime(1);
            Debug.Log("Fading finished");
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
