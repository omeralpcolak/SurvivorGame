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
    public int randomSkillCount;
    public GameObject mainMenuUI;
    public GameObject selectedSkillsUI;

    PlayerController playerController;
    EnemySpawner enemySpawner;
    ScreenManager screenManager;

    public List<TMP_Text> textsOfRandomSkills;
    public List<Image> iconsOfRandomSkills;
    public bool gameStart;
    [SerializeField] private bool canChangeScreenToSkillUpgrade = true;

    private void Start()
    {
        Application.targetFrameRate = 60;
        instance = this;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemySpawner = GetComponent<EnemySpawner>();
        screenManager = GetComponent<ScreenManager>();
    }

    public void GameStart()
    {
        screenManager.ChangeScreen(ScreenManager.Screen.NOMENU);
        gameStart = true;
        playerController.joystick.gameObject.SetActive(true);
        StartCoroutine(enemySpawner.SpawnEnemy(enemySpawner.cooldown));
    }


    public void ChangeScreenToUpgradeSkill()
    {
        var targetScreen = canChangeScreenToSkillUpgrade ? ScreenManager.Screen.SKILLUPGRADEMENU : ScreenManager.Screen.MAINMENU;
        screenManager.ChangeScreen(targetScreen);
        canChangeScreenToSkillUpgrade = !canChangeScreenToSkillUpgrade;

    }

    public void SelectingRandomSkills()
    {
        screenManager.ChangeScreen(ScreenManager.Screen.RANDOMSKILLMENU);
        playerController.skills = SelectRandomSkill(allSkills, randomSkillCount);

        for(int i = 0; i < randomSkillCount; i++)
        {
            textsOfRandomSkills[i].text = playerController.skills[i].skillName;
            iconsOfRandomSkills[i].sprite = playerController.skills[i].skillIcon;
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
