using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Skill> allSkills;
    public List<Skill> selectedSkills;
    public int randomSkillCount;
    public GameObject mainMenuUI;
    public GameObject selectedSkillsUI;
    PlayerController playerController;
    EnemySpawner enemySpawner;

    public List<TMP_Text> skillTexts;
    public bool gameStart;

    private void Start()
    {
        instance = this;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemySpawner = GetComponent<EnemySpawner>();
        //SelectingRandomSkills();
    }

    public void GameStart()
    {
        selectedSkillsUI.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(delegate
        {
            selectedSkillsUI.SetActive(false);
        });
        gameStart = true;
        playerController.joystick.gameObject.SetActive(true);
        StartCoroutine(enemySpawner.SpawnEnemy(enemySpawner.cooldown));
    }


    public void SelectingRandomSkills()
    {
        mainMenuUI.GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(delegate
        {
            mainMenuUI.SetActive(false);
        });

        playerController.skills = SelectRandomSkill(allSkills, randomSkillCount);
        selectedSkillsUI.GetComponent<CanvasGroup>().DOFade(1f, 1f);

        for(int i = 0; i < randomSkillCount; i++)
        {
            skillTexts[i].text = playerController.skills[i].skillName;
        }
       
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
