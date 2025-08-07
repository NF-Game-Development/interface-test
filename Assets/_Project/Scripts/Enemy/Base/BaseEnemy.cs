using UnityEngine;

public class BaseEnemy : MonoExt, IAbilityCastable, IMovable, IRotatable, IDamageable, IAttacker
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
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 movementDirection, MovementStats movementStats)
    {
        throw new System.NotImplementedException();
    }

    public void Rotate(Vector3 rotationDirection, MovementStats movementStats)
    {
        throw new System.NotImplementedException();
    }

    public void ApplyDamage(float damageValue)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(IDamageable damageable)
    {
        throw new System.NotImplementedException();
    }
}
