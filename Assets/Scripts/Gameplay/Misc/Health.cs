
using System;
using UniRx;
using UnityEngine;

public class Health : MonoExt, IDamageable, IHealable
{
    public Subject<Unit> OnDeath { get; } = new Subject<Unit>();
    public float MaxHP;
    public float DestroyOffset;
    public float HP;

    private void Start()
    {
        HP = MaxHP;
    }

    public void ApplyDamage(float damageValue)
    {
        HP -= damageValue;

        if (HP <= 0)
        {
            HP = 0;
            OnDeath.OnNext(Unit.Default);
            Destroy(gameObject, DestroyOffset);
        }
    }

    public void ApplyHeal(float healAmount)
    {
        if (HP >= MaxHP)
            return;
        HP += healAmount;
    }
}