using System;
using NF.Main.Core;
using NF.Main.Core.PlayerStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoExt, IMovable, IRotatable, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private MovementStats _movementStats;
    [TabGroup("References")] [SerializeField] private PlayerInputReader _playerInput;
    [TabGroup("References")] [SerializeField] private Rigidbody _rigidbody;
    [TabGroup("References")] [SerializeField] private Camera _camera;
    [TabGroup("References")] [SerializeField] private ClassType _classType;
    [TabGroup("References")] [SerializeField] private GameObject _centerTransform;
    [TabGroup("References")] [SerializeField] private StateMachine _stateMachine;
    [TabGroup("References")] public PlayerState PlayerState;

    [TabGroup("Ability")] [SerializeField] private AbilityList _abilityList;
    [TabGroup("Ability")] [SerializeField] private AbilityParameterHandler _abilityParameterHandler;
    
    [TabGroup("Debug")] [SerializeField] private bool _canPlayerMove = true;
    [TabGroup("Debug")] [SerializeField] private bool _canPlayerRotate = true;
   
    private Ability _pendingAbility;
    private Vector2 _movementInput = Vector2.zero;
    public Animator _animator;
    private void Awake()
    {
        //Initialize mono extension
        Initialize();
        SetupStateMachine();
    }
    private void Start()
    {
        //Events
        OnSubscriptionSet();
        _playerInput.SetAbilityDictionary(_abilityList.AbilityInputDictionary);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public override void Initialize()
    {
        base.Initialize();
        _playerInput.EnablePlayerActions();
        _abilityList = _classType.AbilityList;
        _abilityList.InitializeAbilities();
        _abilityParameterHandler.Initialize();
    }
     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //Event that handles player movement
        AddEvent(_playerInput.Movement,movementDirection => _movementInput = movementDirection);
        AddEvent(_playerInput.Ability, OnAbilityCast);
    }

    public void FixedUpdate()
    {
        HandleMovement();
    }

    //Handles movement and rotation
    private void HandleMovement()
    {
        if (!_canPlayerMove)
            return;
        if (_abilityParameterHandler.IsAnAbilityExecuting)
            return;
        if (_movementInput.sqrMagnitude > 0.1f)
        {
            PlayerState = PlayerState.Moving;  // Transition to ability state when running
        }
        
        Vector3 normalizedDirection = Utility.CalculateCameraDirection(_camera, _movementInput);
        
        Rotate(normalizedDirection, _movementStats);
        Move(normalizedDirection, _movementStats);
    }


    //Assigns care of players movement
    public void Move(Vector3 movementDirection, MovementStats movementStats)
    {
        Vector3 velocity = movementDirection * movementStats.MovementSpeed;
        _animator.SetFloat("Velocity", velocity.magnitude);
        velocity.y = _rigidbody.linearVelocity.y; // Maintain original vertical velocity (gravity)
        _rigidbody.linearVelocity = velocity;
    }
    

    //Assigns player rotations
    public void Rotate(Vector3 rotationDirection, MovementStats movementStats)
    {
        if (!_canPlayerRotate)
            return;
        
        if (rotationDirection.sqrMagnitude < 0.01f)
            return;
        
        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, movementStats.RotationSmoothTime * Time.fixedDeltaTime));
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
        _abilityList.AbilityDictionary[abilityEnum].OnTriggerAbility(_centerTransform, _abilityParameterHandler);
        _pendingAbility = _abilityList.AbilityDictionary[abilityEnum];
        PlayerState = PlayerState.UsingAbility;

    }
    
    private void SetupStateMachine()
    {
        _stateMachine = new StateMachine();

        var idleState = new PlayerIdleState(this, _animator);
        var abilityState = new PlayerAbilityState(this, _animator);
        var moveState = new PlayerMoveState(this, _animator);

        // Transitions
        Any(idleState, new FuncPredicate(ReturnToIdleState));
        Any(abilityState, new FuncPredicate(IsUsingAbility));
        Any(moveState, new FuncPredicate(IsMoving));

        _stateMachine.SetState(idleState);
    }

    private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

    private bool ReturnToIdleState() => PlayerState == PlayerState.Idle;
    private bool IsUsingAbility() => PlayerState == PlayerState.UsingAbility;
    public Ability GetPendingAbility() => _pendingAbility;
    public Vector2 GetMovementInput() => _movementInput;
    private bool IsMoving() => _movementInput.sqrMagnitude > 0.1f;
    
    public PlayerInputReader GetPlayerInput()
    {
        return _playerInput;
    }

    public AbilityList GetAbilityList()
    {
        return _abilityList;
    }

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    public GameObject GetCenterTransform()
    {
        return _centerTransform;
    }

    public AbilityParameterHandler GetAbilityParameterHandler()
    {
        return _abilityParameterHandler;
    }
}