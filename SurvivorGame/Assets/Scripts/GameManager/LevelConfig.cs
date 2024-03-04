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
}
