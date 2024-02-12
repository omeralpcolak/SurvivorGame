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
    private int totalEnemyCount;
    public void Spawn()
    {
        var enemy = GameObject.Instantiate(enemyPrefab);
        enemy.ownerAttackWaveGroup = this;
        totalEnemyCount++;
    }
    public void Killed()
    {
        totalEnemyCount--;
    }
    public void Check(float percentage)
    {
        while(totalEnemyCount < countCurve.Evaluate(percentage))
        {
            Spawn();
        }
    }
}

public class Level : MonoBehaviour
{
    public List<AttackWave> attackWaves;
    public int waveIndex;
    public AttackWave CurrentWave => attackWaves[waveIndex];

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CurrentWave.currentTime++;
            if(CurrentWave.currentTime >= CurrentWave.duration)
            {
                waveIndex++;
                if(waveIndex >= attackWaves.Count)
                {
                    yield break;
                }
            }
            CurrentWave.Check();
        }
    }
}
