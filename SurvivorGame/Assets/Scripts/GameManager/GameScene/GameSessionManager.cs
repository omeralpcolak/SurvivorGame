using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager instance;

    public List<Skill> allSkills;
  
    public GameObject gameUI;
  
    public Image randomSkillIcon;
    public TMP_Text randomSkillNameTxt;
    public bool gameStart;
    public int coin;

    public RandomSkillPanel randomSkillPanel;
    public GameSelections gameSelections;
    
    private PlayerController playerController;
    private EnemySpawner enemySpawner;
  
    private void Start()
    {
        instance = this;
        gameSelections.InstantiateSelectedObjects();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemySpawner = GetComponent<EnemySpawner>();
    }

    private void OnDisable()
    {
        playerController.skills.Clear();
        gameSelections.UpdateCoinValue(coin);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            randomSkillPanel.Show(RandomSkills(),false);
        }
    }

    public void AddSkill(Skill skill)
    {
        playerController.skills.Add(skill);
    }

    public List<Skill> RandomSkills()
    {
        List<Skill> tempSkills = new(allSkills);
        tempSkills.Shuffle();
        return tempSkills;
    }
    
    private List<Skill> RandomSkillUpgrades()
    {
        List<Skill> tempSkills = new(playerController.skills);
        tempSkills.Shuffle();
        return tempSkills;
    }

    public void StartTheGameSession()
    {
        randomSkillPanel.GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(delegate
        {
            CameraShake.instance.SetThePlayer();
            gameStart = true;
            gameUI.GetComponent<CanvasGroup>().interactable = enabled;
            gameUI.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            StartCoroutine(enemySpawner.SpawnEnemy(gameStart,playerController.transform));
        });
        
    }
}
