using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager instance;
    public List<Skill> allSkills;
    public GameObject joystick;
    public GameObject randomSkillUI;
    public bool gameStart;
    

    private PlayerController playerController;
  
    private void Start()
    {
        instance = this;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void SelectRandomSkill()
    {
        int randomIndex = Random.Range(0, allSkills.Count+1);
        Skill selectedSkill = allSkills[randomIndex];
        playerController.skills.Add(selectedSkill);
    }

    public void StartTheGameSession()
    {
        randomSkillUI.GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(delegate
        {
            gameStart = true;
            joystick.SetActive(true);
        });
        
    }
}
