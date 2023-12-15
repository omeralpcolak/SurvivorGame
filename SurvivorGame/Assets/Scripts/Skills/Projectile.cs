using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile",menuName ="Skill/Projectile")]
public class Projectile : Skill
{
    public GameObject projectilePrefab;

    public override void Activate(Transform spawnPos)
    {
        Instantiate(projectilePrefab, spawnPos.position, Quaternion.LookRotation(spawnPos.forward));
    }
}
