
using System;
using UnityEngine;

public class HealableHealth : Health, IHealable
{
    public void ApplyHeal(float healValue)
    {
        HP += healValue;

        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }
}