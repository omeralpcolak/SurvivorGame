using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Skill", menuName ="Skill")]
public class Skill : ScriptableObject
{
    public string skillName;

    public virtual void Activate(Transform spawnPos)
    {

    }
}
