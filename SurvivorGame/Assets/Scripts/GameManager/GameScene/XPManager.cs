using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class XPManager : MonoBehaviour
{
    public static XPManager instance;

    public Slider xpBar;
    public float maxXp;
    public float currentXp;

    GameSessionManager gameSessionManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameSessionManager = GetComponent<GameSessionManager>();
        currentXp = 0;
        SetXP();
        SetMaxXP();
        
    }

    private void SetXP()
    {
        xpBar.value = currentXp;
        
    }

    private void SetMaxXP()
    {
        xpBar.maxValue = maxXp;
    }

    public void AddXp(float value)
    {

        float totalXp = currentXp + value;

        if (totalXp >= maxXp)
        {
            float remainingXp = totalXp - maxXp;
            LevelUp();
            gameSessionManager.RandomSkillOrUpgradeFunction();
            AddXp(remainingXp);
            return;
        }

        currentXp = totalXp;
        SetXP();
    }

    private void LevelUp()
    {
        currentXp = 0;
        maxXp += 50;
        SetXP();
        SetMaxXP();
    }

    
}
