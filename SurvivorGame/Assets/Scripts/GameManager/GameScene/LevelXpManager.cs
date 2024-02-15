using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelXpManager : MonoBehaviour
{
    public Image xpBarSprite;
    public float maxXp;
    public float currentXp;

    private float target;
    private bool canLevelUp;

    GameSessionManager gameSessionManager;

    private void Start()
    {
        gameSessionManager = GetComponent<GameSessionManager>();
        currentXp = 0;
        UpdateXpBar();
    }

    private void Update()
    {
        if(currentXp < maxXp)
        {
            canLevelUp = true;
        }

        if(currentXp >= maxXp && canLevelUp)
        {
            LevelUp();
            canLevelUp = false;
        }

    }

    public void AddXp(float value)
    {
        currentXp += value;
        UpdateXpBar();
    }

    private void LevelUp()
    {
        gameSessionManager.RandomSkillOrUpgradeFunction();
        currentXp = 0;
        maxXp *= 1.5f;
        UpdateXpBar();
    }

    private void UpdateXpBar()
    {
        target = currentXp / maxXp;
        Debug.Log(currentXp);
        xpBarSprite.fillAmount = target;
    }
}
