using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

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
            for(int i = 0; i<this.skill.level+1; i++)
            {
                Image starIns = Instantiate(star, levelStars);
                allStars.Add(starIns);
            }

            Image lastStar = allStars.Last();
            CanvasGroup lastIndCanvasGrp = lastStar.GetComponent<CanvasGroup>();
            StartCoroutine(FadingStars(lastIndCanvasGrp));

            /*foreach(Image star in allStars)
            {
                CanvasGroup canvasGroup = star.GetComponent<CanvasGroup>();
                StartCoroutine(FadingStars(canvasGroup));
            }*/
        }
    }

    IEnumerator FadingStars(CanvasGroup canvasGroup)
    {
        while (true)
        {
            yield return canvasGroup.DOFade(0f, 0.5f).SetUpdate(UpdateType.Normal, true).WaitForCompletion();

            yield return canvasGroup.DOFade(1f, 0.5f).SetUpdate(UpdateType.Normal, true).WaitForCompletion();
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
