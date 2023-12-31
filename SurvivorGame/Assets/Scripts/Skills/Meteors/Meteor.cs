using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Area Skill", menuName ="Skill/Meteor")]
public class Meteor : Skill
{
    
    public GameObject areaSkillPrefab;
    public Vector3 maxScale;
    public float spawnRadius;
    public float timePerDamage;

    public override void Activate(Transform playerTransform)
    {
        Vector3 randomPos = RandomPointAroundPlayer(playerTransform.position, spawnRadius);
        GameObject meteorInstance = Instantiate(areaSkillPrefab, randomPos, Quaternion.identity);
        AddController(this.skillType, meteorInstance);
        meteorInstance.GetComponent<MeteorController>().MeteorUpgrade(maxScale, damage,timePerDamage);

    }

    private Vector3 RandomPointAroundPlayer(Vector3 center, float radius)
    {
        Vector3 randomPos = center + Random.insideUnitSphere * radius;
        randomPos.y = center.y;
        return randomPos;
    }

    public override void Upgrade()
    {
        damage += this.increaseAmount;
        timePerDamage -= this.increaseAmount / .5f;
        maxScale.x += this.increaseAmount * .5f;
        maxScale.y += this.increaseAmount * .5f;
        maxScale.z += this.increaseAmount * .5f;
        this.cooldownDuration /= 2;
    }
}
