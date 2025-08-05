using System;
using NF.Main.Core.PlayerStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoExt, IMovable, IRotatable, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private MovementStats _movementStats;
    [TabGroup("References")] public PlayerInputReader PlayerInput;
    [TabGroup("References")] [SerializeField] private PlayerAnimation _playerAnimation;
    [TabGroup("References")] [SerializeField] private Rigidbody _rigidbody;
    [TabGroup("References")] [SerializeField] private Camera _camera;

    [TabGroup("Ability")] [SerializeField] private AbilityList _abilityList;
    [TabGroup("Ability")] [SerializeField] private AbilityParameterHandler _abilityParameterHandler;
    
    [TabGroup("Debug")] public bool CanPlayerMove = true;
    [TabGroup("Debug")] public bool CanPlayerRotate = true;
    [TabGroup("Debug")] public bool CanAttack = true;
    [TabGroup("Debug")] public float BasicAttackCoolDown = 0.5f;
    
    private IAttackPerformer _iAttackPerformer;
    
    private Vector2 _movementInput = Vector2.zero;
    
    private void Awake()
    {
        //Initialize mono extension
        Initialize();
    }
    private void Start()
    {
        //Events
        OnSubscriptionSet();
    }
    
    public override void Initialize()
    {
        base.Initialize();
        PlayerInput.EnablePlayerActions();
        _abilityList.InitializeAbilities();
        _abilityParameterHandler.Initialize();
    }
     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //Event that handles player movement
        AddEvent(PlayerInput.Movement,movementDirection => _movementInput = movementDirection);
        AddEvent(PlayerInput.Ability, OnAbilityCast);
        AddEvent(PlayerInput.BasicAttack, _ => HandleBasicAttack());
    }
    
    public void SetAttackPerformer(IAttackPerformer attackPerformer)
    {
        _iAttackPerformer = attackPerformer;
        
        Debug.LogWarning(attackPerformer);
    }

    public void HandleBasicAttack()
    {
        //Prevents Button Spamming
        if (!CanAttack || _playerAnimation.PlayerState == PlayerState.BasicAttack)
            return;
        
        CanAttack = false;
        
        _iAttackPerformer?.PerformAttack();
        AssignNewState(PlayerState.BasicAttack);
        
        Invoke(nameof(ResetAttack), BasicAttackCoolDown);
    }
    
    private void ResetAttack()
    {
        CanAttack = true;
    }

    public void AssignNewState(PlayerState newState)
    {
        _playerAnimation.SetState(newState);
    }
    
    //Handles movement and rotation
    public void HandleMovement()
    {
        if (!CanPlayerMove)
            return;
        
        Vector3 normalizedDirection = Utility.CalculateCameraDirection(_camera, _movementInput);
        
        Rotate(normalizedDirection, _movementStats);
        Move(normalizedDirection, _movementStats);
    }
    
    
    //Assigns care of players movement
    public void Move(Vector3 movementDirection, MovementStats movementStats)
    {
        Vector3 velocity = movementDirection * movementStats.MovementSpeed;
        velocity.y = _rigidbody.linearVelocity.y; // Maintain original vertical velocity (gravity)
        
        _rigidbody.linearVelocity = velocity;
    }
    

    //Assigns player rotations
    public void Rotate(Vector3 rotationDirection, MovementStats movementStats)
    {
        if (!CanPlayerRotate)
            return;
        
        if (rotationDirection.sqrMagnitude < 0.01f)
            return;
        
        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, movementStats.RotationSmoothTime * Time.fixedDeltaTime));
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
        _abilityList.AbilityDictionary[abilityEnum].OnTriggerAbility(gameObject, _abilityParameterHandler);
    }
    
    public void InjectDependencies(PlayerControllerDependencies playerControllerDependencies)
    {
        _movementStats = playerControllerDependencies.MovementStats;
        _abilityList = playerControllerDependencies.AbilityList;
        _abilityParameterHandler = playerControllerDependencies.ParameterHandler;
    }
}