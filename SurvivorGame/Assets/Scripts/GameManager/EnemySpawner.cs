using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    public float cooldown;
    public float radius;


    void Start()
    {
        StartCoroutine(SpawnEnemy(cooldown));
    }

    private void Update()
    {
        
    }


    public IEnumerator SpawnEnemy(float cooldown)
    {
        Vector3 spawnPos = RandomCircle(player.transform.position, radius);
        Instantiate(enemy, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(SpawnEnemy(cooldown));
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
