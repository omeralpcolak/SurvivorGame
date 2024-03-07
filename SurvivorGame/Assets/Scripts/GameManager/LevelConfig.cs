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

    public void LevelCompleteSave(int levelId)
    {
        PlayerPrefs.SetInt(levelId.ToString() + "_Completed",1);
        PlayerPrefs.Save();
    }

    public bool isLevelCompleted(int levelId)
    {
        return PlayerPrefs.GetInt(levelId.ToString() + "_Completed", 0) == 1;
    }


}
