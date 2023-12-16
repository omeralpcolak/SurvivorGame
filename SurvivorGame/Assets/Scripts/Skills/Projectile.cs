using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile",menuName ="Skill/Projectile")]
public class Projectile : Skill
{
    public GameObject projectilePrefab;
    public int increaseAmount;
    public float projectileSpeed;

    public override void Activate(Transform spawnPos)
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, spawnPos.position, Quaternion.LookRotation(spawnPos.forward),spawnPos);

        ProjectileController projectileController = projectileInstance.GetComponent<ProjectileController>();

        if(projectileController != null)
        {
            projectileController.ProjectileUpgrade(damage,projectileSpeed);
        }
        
    }

    public override void Upgrade()
    {
        damage += increaseAmount;
        projectileSpeed += increaseAmount;
    }
}
