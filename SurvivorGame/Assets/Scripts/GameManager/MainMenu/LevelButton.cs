using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Level levelPrefab;
    public LevelConfig levelConfig;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        levelConfig.levelPrefab = levelPrefab;
        MainMenuManager.instance.PopUpBubble(levelPrefab.name + " is selected", 0.4f);
        
    }
}
