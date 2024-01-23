using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float cooldown;
    public float radius;
    public int numberOfEnemy;


    /*public IEnumerator SpawnEnemy(float cooldown, bool gameStart,Transform center)
    {
        while (gameStart)
        {
            for(int i = 0; i < numberOfEnemy; i++)
            {
                Vector3 spawnPos = RandomCircle(center.position, radius);
                Instantiate(enemy, spawnPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }

    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }*/

    public IEnumerator SpawnEnemy(bool gameStart, Transform center)
    {
        while (gameStart)
        {
            for (int i = 0; i < numberOfEnemy; i++)
            {
                Vector3 spawnPos = RandomCircle(center.position, radius);
                Instantiate(enemy, spawnPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }

    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

}
