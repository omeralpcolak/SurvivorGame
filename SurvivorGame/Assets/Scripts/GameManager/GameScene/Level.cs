using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackWave
{
    public float duration;
    public float currentTime;
    public List<AttackWaveGroup> groups;
    public void Check()
    {
        groups.ForEach(x => x.Check(currentTime/duration));
    }
}

[System.Serializable]
public class AttackWaveGroup
{   
    public EnemyController enemyPrefab;
    public AnimationCurve countCurve;
    [SerializeField]public int totalEnemyCount;
    public int spawnRadius;
    public void Spawn()
    {
        Vector3 RandomCircle(Vector3 center, float radius)
        {
            float ang = Random.value * 360;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y;
            pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            return pos;
        }

        Vector3 spawnPos = RandomCircle(GameObject.FindGameObjectWithTag("Player").transform.position, spawnRadius);

        var enemy = GameObject.Instantiate(enemyPrefab,spawnPos,Quaternion.identity);
        enemy.ownerAttackWaveGroup = this;
        totalEnemyCount++;
    }
    public void Killed()
    {
        totalEnemyCount--;
    }
    public void Check(float ratio)
    {
        while(totalEnemyCount < countCurve.Evaluate(ratio))
        {
            Spawn();
        }
    }
}


public class Level : MonoBehaviour
{
    public static Level instance;
    public List<AttackWave> attackWaves;
    public int waveIndex;
    public int levelId;
    public AttackWave CurrentWave => attackWaves[waveIndex];


    private void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        while (GameSessionManager.instance.gameStart)
        {
            yield return new WaitForSeconds(1f);
            CurrentWave.currentTime++;
            if (CurrentWave.currentTime >= CurrentWave.duration)
            {
                waveIndex++;
                if (waveIndex >= attackWaves.Count)
                {
                    yield break;
                }
            }
            CurrentWave.Check();
        }
    }

    public void CheckLevelComplete()
    {
        if (!AllEnemiesDead())
        {
            return;
        }
        GameSessionManager.instance.levelConfig.LevelCompleteSave(levelId);
        GameSessionManager.instance.GameComplete(true);
    }

    private bool AllEnemiesDead()
    {
        foreach (AttackWave wave in attackWaves)
        {
            foreach (AttackWaveGroup group in wave.groups)
            {
                if (group.totalEnemyCount > 0)
                    return false;
            }
        }
        return true;
    }

}
