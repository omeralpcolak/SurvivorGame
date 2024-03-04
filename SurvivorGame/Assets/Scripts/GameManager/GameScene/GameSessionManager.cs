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
    private PlayerController playerController;

    public List<Skill> allSkills;

    public TMP_Text inGameCoinTxt; //during game session
    public TMP_Text totalCoinText;
    public TMP_Text earnedCoinText; // game over screen

    public GameObject gameUI;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    public Button backToMenuBtn;
  
    public bool gameStart;
    public int coin = 0;
    [SerializeField] private int maxSkillCount; // maximum number of skills that player can own.

    private bool isItUpgrade;
    private bool isItFirstTime = true;

    public RandomSkillPanel randomSkillPanel;
    public GameSelections gameSelections;
    public LevelConfig levelConfig;
    
   
  
    private void Start()
    {
        instance = this;
        gameSelections.InstantiateSelectedObjects();
        levelConfig.InsTheLevel();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartTheGameSession();
    }

    public void AddAndUpdateInGameCoinValue()
    {
        coin += 10;
        inGameCoinTxt.text = "Coin: " + coin.ToString();
    }


    public void AddSkill(Skill skill)
    {
        playerController.skills.Add(skill);
    }

    public void GameOver()
    {
        Debug.Log("GAME IS OVER");
        gameStart = false;
        gameUI.SetActive(false);
        gameOverScreen.SetActive(true);

        if(coin == 0)
        {
            earnedCoinText.text = 0.ToString();
            totalCoinText.text = "Total Coin: " + gameSelections.coin.ToString();
            backToMenuBtn.interactable = true;
        }
        else
        {
            StartCoroutine(CoinAnim(1, 0.2f));
        }
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
            return tempSkills.Take(3).ToList();
        }

        randomSkillPanel.Show(RandomSkillOrUpgrade(isItUpgrade), isItUpgrade);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
        gameSelections.ResetSelectedObjects();
        levelConfig.ClearTheLevelPrefab();
        Time.timeScale = 1;
    }


    IEnumerator CoinAnim(int addingCoinAmount, float addingSpeed)
    {
       
        int targetCoinValue = coin + gameSelections.coin;

        while(gameSelections.coin < targetCoinValue)
        {
            Debug.Log("routine is working");
            gameSelections.coin += addingCoinAmount;
            coin -= addingCoinAmount;
            
            if(gameSelections.coin > targetCoinValue)
            {
                gameSelections.coin = targetCoinValue;
            }

            if(coin < 0)
            {
                coin = 0;
            }

            totalCoinText.text = "Total coin: " + gameSelections.coin.ToString();
            earnedCoinText.text = coin.ToString();
            yield return new WaitForSeconds(addingSpeed);
            addingSpeed -= 0.01f;
        }
        Debug.Log("coin is saved");
        gameSelections.UpdateCoinValue(targetCoinValue);
        backToMenuBtn.interactable = true;
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
