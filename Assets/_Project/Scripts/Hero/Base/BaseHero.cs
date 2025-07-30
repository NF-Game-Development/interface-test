using System;
using UnityEngine;

public class BaseHero : MonoExt, IAbilityCastable, IMovable, IRotatable, IDamageable, IAttacker, IHealable
{
    [SerializeField] private HeroScriptableObject _heroScriptableObject;
    [SerializeField] private PlayerController _playerController;
    
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
        
        _playerController.InjectDependencies(new PlayerControllerDependencies
        {
            MovementStats = _heroScriptableObject.MovementStats,
            AbilityList = _heroScriptableObject.AbilityList,
            ParameterHandler = _heroScriptableObject.AbilityParameterHandler
        });
        
        _playerController.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
        _playerController.OnAbilityCast(abilityEnum);
    }

    public void Move(Vector3 movementDirection, MovementStats movementStats)
    {
        _playerController.Move(movementDirection, movementStats);
    }

    public void Rotate(Vector3 rotationDirection, MovementStats movementStats)
    {
        _playerController.Rotate(rotationDirection, movementStats);
    }

    public void ApplyDamage(float damageValue)
    {
        throw new NotImplementedException();
    }

    public virtual void Attack(IDamageable damageable)
    {
        throw new NotImplementedException();
    }

    public virtual void ApplyHeal(int addHealth)
    {
        throw new NotImplementedException();
    }
}
