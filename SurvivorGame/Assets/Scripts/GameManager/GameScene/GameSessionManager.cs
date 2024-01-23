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
    public List<Skill> allSkillUpgrades;
    public List<Button> selectionButtons;
    public GameObject gameUI;
    public GameObject randomSkillPanel;
    public Image randomSkillIcon;
    public TMP_Text randomSkillNameTxt;
    public bool gameStart;
    public int coin;

    public GameSelections gameSelections;
    
    private PlayerController playerController;
    private EnemySpawner enemySpawner;
  
    private void Start()
    {
        instance = this;
        gameSelections.InstantiateSelectedObjects();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemySpawner = GetComponent<EnemySpawner>();
        SelectRandomSkill();
    }

    private void OnDisable()
    {
        playerController.skills.Clear();
        gameSelections.UpdateCoinValue(coin);
    }

    private void RandomSkills()
    {
        for (int i = 0; i < 3; i++)
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

            selectionButtons[i].GetComponent<Image>().sprite = selectedSkill.skillProperty.icon;
            selectionButtons[i].onClick.AddListener(() => playerController.skills.Add(selectedSkill));

        }
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

    

    public void StartTheGameSession()
    {
        randomSkillPanel.GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(delegate
        {
            CameraShake.instance.SetThePlayer();
            randomSkillPanel.SetActive(false);
            gameStart = true;
            gameUI.GetComponent<CanvasGroup>().interactable = enabled;
            gameUI.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            StartCoroutine(enemySpawner.SpawnEnemy(gameStart,playerController.transform));
        });
        
    }
}
