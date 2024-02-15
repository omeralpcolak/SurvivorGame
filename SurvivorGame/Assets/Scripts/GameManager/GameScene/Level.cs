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
    [SerializeField]private int totalEnemyCount;
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
