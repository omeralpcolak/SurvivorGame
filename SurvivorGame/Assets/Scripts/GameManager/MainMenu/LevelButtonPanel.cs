using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelButtonPanel : MonoBehaviour
{
    public List<LevelButton> levelButtons;
    public LevelConfig levelConfig;

    private void Start()
    {
        levelButtons.ForEach(x => x.Init(this));
        CheckLevelButtons();
    }

    public void CheckLevelButtons()
    {
        for (int i = 1; i < levelButtons.Count; i++)
        {
            if (levelConfig.isLevelCompleted(levelButtons[i-1].levelPrefab.levelId))
            {
                levelButtons[i].button.interactable = true;
            }
            else
            {
                levelButtons[i].button.interactable = false;
            }
        }
    }
}
