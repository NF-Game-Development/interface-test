
using System;
using UniRx;
using UnityEngine;

public class Health : MonoExt, IDamageable, IHealable
{
    //[SerializeField] private Stat MaxHealth;
    
    public Subject<Unit> onDestroyed;

    public float MaxHP;
    public float HP;

    private void Awake()
    {
        onDestroyed = new Subject<Unit>();
    }

    private void Start()
    {
        HP = MaxHP;
    }

    public void ApplyDamage(float damageValue)
    {
        HP -= damageValue;

        if (HP <= 0)
        {
            onDestroyed.OnNext(Unit.Default);
            Destroy(gameObject, 0.2f);
        }
    }

    public void ApplyHeal(float healAmount)
    {
        if (HP >= MaxHP)
            return;
        HP += healAmount;
    }
}