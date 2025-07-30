using System;
using UnityEngine;

public class BaseHero : MonoExt, IAbilityCastable, IMovable, IRotatable, IDamageable, IAttacker, IHealable
{
    private void Awake()
    {
        Initialize();
    }
    private void Start()
    {
        OnSubscriptionSet();
    }
    
    public override void Initialize()
    {
        base.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
        throw new NotImplementedException();
    }

    public void Move(Vector3 movementDirection, MovementStats movementStats)
    {
        throw new NotImplementedException();
    }

    public void Rotate(Vector3 rotationDirection, MovementStats movementStats)
    {
        throw new NotImplementedException();
    }

    public void ApplyDamage(float damageValue)
    {
        throw new NotImplementedException();
    }

    public void Attack(IDamageable damageable)
    {
        throw new NotImplementedException();
    }

    public void ApplyHeal(int addHealth)
    {
        throw new NotImplementedException();
    }
}
