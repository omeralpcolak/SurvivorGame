using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager instance;
    public List<Skill> allSkills;
    public GameObject joystick;
    public GameObject randomSkillPanel;
    public Image randomSkillIcon;
    public TMP_Text randomSkillNameTxt;
    public bool gameStart;
    

    private PlayerController playerController;
    private EnemySpawner enemySpawner;
  
    private void Start()
    {
        instance = this;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemySpawner = GetComponent<EnemySpawner>();
        SelectRandomSkill();
    }

    private void SelectRandomSkill()
    {
        Skill selectedSkill = null;
        bool isSkillAlreadyOwned = true;

        while (isSkillAlreadyOwned)
        {
            int randomIndex = Random.Range(0, allSkills.Count);
            selectedSkill = allSkills[randomIndex];

            if (!playerController.skills.Contains(selectedSkill))
            {
                isSkillAlreadyOwned = false;
            }
        }

        playerController.skills.Add(selectedSkill);
        randomSkillIcon.sprite = selectedSkill.skillProperty.icon;
        randomSkillNameTxt.text = selectedSkill.skillProperty.name;
        randomSkillPanel.GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }

    private void OnDisable()
    {
        playerController.skills.Clear();
    }

    public void StartTheGameSession()
    {
        randomSkillPanel.GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(delegate
        {
            randomSkillPanel.SetActive(false);
            gameStart = true;
            joystick.SetActive(true);
            StartCoroutine(enemySpawner.SpawnEnemy(enemySpawner.cooldown,gameStart,playerController.transform));
        });
        
    }
}
