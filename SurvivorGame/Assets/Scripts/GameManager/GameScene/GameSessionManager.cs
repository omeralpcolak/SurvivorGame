using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using System.Linq;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager instance;

    public List<Skill> allSkills;

    public TMP_Text inGameCoinTxt;
    public GameObject gameUI;
    public GameObject pauseScreen;
  
    public bool gameStart;
    public int coin;
    [SerializeField] private int maxSkillCount; // maximum number of skills that player can own.

    public bool isItUpgrade;
    public bool isItFirstTime = true;

    public RandomSkillPanel randomSkillPanel;
    public GameSelections gameSelections;
    
    private PlayerController playerController;
  
    private void Start()
    {
        instance = this;
        gameSelections.InstantiateSelectedObjects();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartTheGameSession();
    }

    public void AddAndUpdateInGameCoinValue()
    {
        coin++;
        inGameCoinTxt.text = "Coin: " + coin.ToString();
    }

    private void OnDisable()
    {
        playerController.skills.Clear();
        gameSelections.UpdateCoinValue(coin);
    }
    

    public void AddSkill(Skill skill)
    {
        playerController.skills.Add(skill);
    }

    public void GameOver()
    {
        Debug.Log("GAME IS OVER");
        gameStart = false;
    }

    public void RandomSkillOrUpgradeFunction()
    {
        bool FlipCoin()
        {
            int randomNumber = Random.Range(0, 2);
            return randomNumber == 1;
        }

        if (isItFirstTime)
        {
            isItUpgrade = false;
            isItFirstTime = false;
        }
        else
        {
            bool canAnySkillBeUpgrade = playerController.skills.Any(x => x.canBeUpgraded);

            if (canAnySkillBeUpgrade)
            {
                isItUpgrade = playerController.skills.Count >= maxSkillCount ? true : FlipCoin();
            }
            else
            {
                isItUpgrade = false;
            }
        }

        List<Skill> RandomSkillOrUpgrade(bool isItUpgrade)
        {
            List<Skill> tempSkills = isItUpgrade ? new List<Skill>(playerController.skills.FindAll(x => x.canBeUpgraded)) : allSkills.FindAll(x => !x.isOwned);
            tempSkills.Shuffle();
            return tempSkills.GetRange(0,3);
        }

        randomSkillPanel.Show(RandomSkillOrUpgrade(isItUpgrade), isItUpgrade);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1;
    }


    public void ResumeAndPause()
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        pauseScreen.SetActive(Time.timeScale == 0);
    }

    private void StartTheGameSession()
    {
        RandomSkillOrUpgradeFunction();
        CameraShake.instance.SetThePlayer();
        gameStart = true;
        gameUI.GetComponent<CanvasGroup>().interactable = enabled;
        gameUI.GetComponent<CanvasGroup>().DOFade(1, 0.5f);    
    }
}
