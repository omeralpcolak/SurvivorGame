using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelXpManager : MonoBehaviour
{
    public Image xpBarSprite;
    public float maxXp;
    public float currentXp;

    private float target = 1;
    private float addSpeed = .8f; //xp adding speed (visual)

    private void Start()
    {
        currentXp = 0;
        UpdateXpBar();
    }

    private void Update()
    {
        xpBarSprite.fillAmount = Mathf.MoveTowards(xpBarSprite.fillAmount, target, addSpeed * Time.deltaTime);
    }

    public void AddXp(float value)
    {
        currentXp += value;
        if(currentXp >= maxXp)
        {
            LevelUp();
        }
        UpdateXpBar();
    }

    private void LevelUp()
    {
        currentXp = 0;
        maxXp *= 1.5f;
    }

    private void UpdateXpBar()
    {
        target = currentXp / maxXp;
    }
}
