using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Skill> allSkills;
    public List<Skill> selectedSkills;
    public int randomSkillCount;
    
    public bool gameStart;

    private void Start()
    {
        instance = this;
    }

    public void GameStart()
    {
        gameStart = true;
    }


    private void SelectingRandomSkills()
    {
        selectedSkills = SelectRandomSkill(allSkills, randomSkillCount);
    }

    List<Skill> SelectRandomSkill(List<Skill> sampleList, int count)
    {
        HashSet<int> selectedIndexes = new HashSet<int>();
        List<Skill> selectedSkills = new List<Skill>();

        while(selectedSkills.Count < count)
        {
            int randomIndex = Random.Range(0, sampleList.Count);
            if (selectedIndexes.Add(randomIndex))
            {
                selectedSkills.Add(sampleList[randomIndex]);
            }

        }

        return selectedSkills;
    }


}
