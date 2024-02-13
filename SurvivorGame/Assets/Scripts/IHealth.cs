using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHealth
{
    public int MaxHealth { get; }
    public int CurrentHealth { get; }

    void TakeDamage(int damageAmount);
}
