
using System;
using UnityEngine;

public class Health : MonoExt, IDamageable, IHealable
{
    //[SerializeField] private Stat MaxHealth;

    public float MaxHP;
    public float HP;

    private void Awake()
    {
    }

    private void Start()
    {
        HP = MaxHP;
    }

    public void ApplyDamage(float damageValue)
    {
        HP -= damageValue;

        if (HP <= 0)
            Destroy(gameObject, 0.2f);
    }

    public void ApplyHeal(float healAmount)
    {
        if (HP >= MaxHP)
            return;
        HP += healAmount;
    }
}