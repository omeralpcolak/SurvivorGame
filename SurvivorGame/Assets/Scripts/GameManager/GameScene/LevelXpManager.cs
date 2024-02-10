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
    private float addSpeed = 10; //xp adding speed (visual)
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
        
        xpBarSprite.fillAmount = Mathf.MoveTowards(xpBarSprite.fillAmount, target, addSpeed * Time.deltaTime);

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

        /*xpBarSprite.DOFillAmount(target, addSpeed).OnComplete(delegate
        {
            gameSessionManager.RandomSkillOrUpgradeFunction();
        });*/

        // I think there are some issues here ...
        
        
    }

    private void UpdateXpBar()
    {
        target = currentXp / maxXp;
        //xpBarSprite.DOFillAmount(target, addSpeed);
    }
}
