using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Config", menuName = "Character Config")]
public class CharacterConfig : ScriptableObject
{
    public string characterName;
    public int movementSpeed;
    public int maxHealth;
}
