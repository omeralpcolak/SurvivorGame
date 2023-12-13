using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Player Config",menuName ="Player Config")]
public class PlayerConfig : ScriptableObject
{
    public string playerName;
    public int maxHealth;
    public float attackSpeed;
    public float movementSpeed;
}
