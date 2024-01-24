using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkillPanel : MonoBehaviour
{
    public Transform skillContents;
    public RandomSkillItem randomSkillItemPrefab;

    public void Show(List<Skill> tempList, bool boolean)
    {
        gameObject.SetActive(true);

        skillContents.DestroyAllChildren();

        for(int i = 0; i<tempList.Count; i++)
        {
            RandomSkillItem item = Instantiate(randomSkillItemPrefab, skillContents);
            item.Init(this,tempList[i],boolean);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
