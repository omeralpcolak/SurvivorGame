using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile",menuName ="Skill/Projectile")]
public class Projectile : Skill
{
    public GameObject projectilePrefab;
    public float projectileSpeed;

    public override void Activate(Transform spawnPos)
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, spawnPos.position, Quaternion.identity, spawnPos);
        AddController(this.skillType, projectileInstance);
        projectileInstance.GetComponent<ProjectileController>().ProjectileUpgrade(damage, projectileSpeed);
    }

    public override void Upgrade()
    {
        damage += this.increaseAmount;
        this.cooldownDuration /= 2;
    }
}
