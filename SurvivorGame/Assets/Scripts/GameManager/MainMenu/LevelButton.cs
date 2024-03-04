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
        button.interactable = false;
        
    }
}
