using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Level levelPrefab;
    public LevelConfig levelConfig;
    [HideInInspector] public Button button;
    public bool completed;

    public void Init()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        completed = levelConfig.isLevelCompleted(levelPrefab.levelId);
    }

    public void OnClick()
    {
        levelConfig.levelPrefab = levelPrefab;
        MainMenuManager.instance.PopUpBubble(levelPrefab.name + " is selected", 0.4f);
    }
}
