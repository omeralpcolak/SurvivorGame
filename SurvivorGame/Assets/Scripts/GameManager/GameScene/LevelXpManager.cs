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

    private float target = 1;
    private float addSpeed = .8f; //xp adding speed (visual)

    GameSessionManager gameSessionManager;

    private void Start()
    {
        gameSessionManager = GetComponent<GameSessionManager>();
        currentXp = 0;
        UpdateXpBar();
    }

    private void Update()
    {
        //xpBarSprite.fillAmount = Mathf.MoveTowards(xpBarSprite.fillAmount, target, addSpeed * Time.deltaTime);
    }

    public void AddXp(float value)
    {
        currentXp += value;
        UpdateXpBar();
        if (currentXp >= maxXp)
        {
            LevelUp();
        }
        
    }

    private void LevelUp()
    {
        currentXp = 0;
        maxXp *= 1.5f;
        xpBarSprite.DOFillAmount(target, addSpeed).OnComplete(delegate
        {
            gameSessionManager.RandomSkillOrUpgradeFunction();
        });

        // I think there are some issues here ...
        
        
    }

    private void UpdateXpBar()
    {
        target = currentXp / maxXp;
        xpBarSprite.DOFillAmount(target, addSpeed);
    }
}
