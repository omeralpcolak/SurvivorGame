using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Skill", menuName ="Skill/Melee")]
public class Melee : Skill
{
    public float spinningSpeed;
    public GameObject meleePrefab;
    public bool canCreated;

    private void OnEnable()
    {
        canCreated = true;
    }

    public override void Activate(Transform spawnPos)
    {
        if (canCreated)
        {
            canCreated = false;
            GameObject meleeInstance = Instantiate(meleePrefab, spawnPos.position, Quaternion.identity, spawnPos);
            AddController(this.skillType, meleeInstance);
            meleeInstance.GetComponent<MeleeController>().MeleeUpgrade(damage, spinningSpeed);
        }
        
        
    }

    public override void Upgrade()
    {
        damage += this.increaseAmount;
        spinningSpeed += this.increaseAmount;
    }
}
