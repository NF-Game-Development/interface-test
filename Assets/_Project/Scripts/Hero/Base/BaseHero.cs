using System;
using UnityEngine;
using UnityEngine.Serialization;
using Sirenix.OdinInspector;

public class BaseHero : MonoExt, IAbilityCastable, IMovable, IRotatable, IDamageable, IAttacker, IHealable
{
    [TabGroup("Scriptable Hero")] public HeroScriptableObject HeroScriptableObject;
    [TabGroup("Player Controller")] [SerializeField] private PlayerController _playerController;
    
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
        
        InitializePlayerController();
        InitializeInjectDependencies();
    }

    private void InitializePlayerController()
    {
        _playerController.Initialize();
    }

    private void InitializeInjectDependencies()
    {
        _playerController.InjectDependencies(new PlayerControllerDependencies
        {
            MovementStats = HeroScriptableObject.MovementStats,
            AbilityList = HeroScriptableObject.AbilityList,
            ParameterHandler = HeroScriptableObject.AbilityParameterHandler
        });
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

    public virtual void ApplyDamage(float damageValue)
    {
        
    }

    public virtual void Attack(IDamageable damageable)
    {
        
    }

    public virtual void ApplyHeal(int addHealth)
    {
        throw new NotImplementedException();
    }
}
