using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Skill", menuName ="Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public int damage;

    public virtual void Activate(Transform spawnPos)
    {

    }

    public virtual void Upgrade()
    {

    }
}
