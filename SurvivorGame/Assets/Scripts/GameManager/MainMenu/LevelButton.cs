using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public TextAsset levelJSONFile;

    private void Start()
    {
        
    }

    public void OnClick()
    {
        MainMenuManager.instance.gameSelections.levelJSONFile = levelJSONFile;
    }
}
