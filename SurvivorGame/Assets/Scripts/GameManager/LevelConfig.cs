using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Config", menuName = "LevelConfig")]
public  class LevelConfig : ScriptableObject
{
    public Level levelPrefab;

    public void SetTheLevel(Level _levelPrefab)
    {
        levelPrefab = _levelPrefab;
    }

    public void InsTheLevel()
    {
        Instantiate(levelPrefab);
    }

    public void ClearTheLevelPrefab()
    {
        levelPrefab = null;
    }

    public void LevelCompleteSave(string levelName)
    {
        PlayerPrefs.SetInt(levelName + "_Completed", 1);
    }

    public bool isLevelCompleted(string levelName)
    {
        return PlayerPrefs.GetInt(levelName + "_Compeleted", 0) == 1;
    }


}
