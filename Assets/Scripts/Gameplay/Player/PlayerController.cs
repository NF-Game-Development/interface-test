using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using NF.Main.Core;
using NF.Main.Core.PlayerStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoExt, IMovable, IRotatable, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private MovementStats _movementStats;
    [TabGroup("References")] [SerializeField] private PlayerInputReader _playerInput;
    [TabGroup("References")] [SerializeField] private Animator _animator;
    [TabGroup("References")] [SerializeField] private Rigidbody _rigidbody;
    [TabGroup("References")] [SerializeField] private Camera _camera;
    
    [TabGroup("Ability")] [SerializeField] private AbilityParameterHandler _abilityParameterHandler;
    
    [TabGroup("Player")] [SerializeField] private PlayerUnit _player;
    
    [TabGroup("Debug")] [SerializeField] private bool _canPlayerMove = true;
    [TabGroup("Debug")] [SerializeField] private bool _canPlayerRotate = true;

    [TabGroup("b", "Interactables")] 
    [SerializeField] private LayerMask _interactableLayerMask;
    [TabGroup("b", "Interactables")] 
    [SerializeField] private Stat _interactDistance;
    [TabGroup("b", "Interactables")] 
    [SerializeField] private IInteractable _currentInteractable;
    
    //State
    private StateMachine _stateMachine;
    private Dictionary<int, PlayerState> _playerAbilityState = new Dictionary<int, PlayerState>();
    public PlayerState PlayerState { get; set; }
    
    private Vector2 _movementInput = Vector2.zero;
    //private IAbilityCastable _abilityCastableImplementation;
    
    private AbilityAnimationDictionary _abilityAnimationDictionary;

    private void Awake()
    {
        //Initialize mono extension
        Initialize();
        //SetupStateMachine();
    }
    private void Start()
    {
        //Events
        OnSubscriptionSet();
    }
    
    public override void Initialize()
    {
        base.Initialize();
        _playerInput.EnablePlayerActions();
        _abilityParameterHandler.Initialize();
    }
     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //Event that handles player movement
        AddEvent(_playerInput.Movement,movementDirection => _movementInput = movementDirection);
        AddEvent(_playerInput.Movement, TransitionToMoveState);
        AddEvent(_playerInput.Ability, OnAbilityCast);
        AddEvent(_abilityParameterHandler.AbilityCasted, DisableMovement);
        AddEvent(_abilityParameterHandler.AbilityEnded, DisableMovement);
        AddEvent(_playerInput.Interact, _ => OnInteract());
    }

    public void FixedUpdate()
    {
        //HandleMovement();
        _stateMachine.FixedUpdate();
    }

    public void Update()
    {
        _stateMachine.Update();
    }

    private void SetupStateMachine()
    {
        // State Machine
        _stateMachine = new StateMachine();
            
        // Declare Player States
        var idleState = new PlayerIdleState(this, _animator);
        var moveState = new PlayerMoveState(this, _animator);
        var ability1State = new PlayerAbility1State(this, _animator);
        var ability2State = new PlayerAbility2State(this, _animator);
        var ability3State = new PlayerAbility3State(this, _animator);
            
        // Define Player State Transitions
        Any(idleState, new FuncPredicate(ReturnToIdleState));
        Any(moveState, new FuncPredicate(() => PlayerState == PlayerState.Moving));
        Any(ability1State, new FuncPredicate(() => PlayerState == PlayerState.Ability1));
        Any(ability2State, new FuncPredicate(() => PlayerState == PlayerState.Ability2));
        Any(ability3State, new FuncPredicate(() => PlayerState == PlayerState.Ability3));
        
        _playerAbilityState.Add(1, PlayerState.Ability1);
        _playerAbilityState.Add(2, PlayerState.Ability2);
        _playerAbilityState.Add(3, PlayerState.Ability3);
            
        // Set Initial State
        _stateMachine.SetState(idleState);
    }

    private bool ReturnToIdleState()
    {
        return PlayerState == PlayerState.Idle;
    }

    private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);


    //Handles movement and rotation
    public void HandleMovement()
    {
        if (!_canPlayerMove)
            return;
        
        Vector3 normalizedDirection = Utility.CalculateCameraDirection(_camera, _movementInput);
        
        Rotate(normalizedDirection, _movementStats);
        Move(normalizedDirection, _movementStats);
    }

    private void TransitionToMoveState(Vector2 direction)
    {
        if (direction != Vector2.zero && (_canPlayerMove == true && _canPlayerRotate == true))
        {
            PlayerState = PlayerState.Moving;
        }
        else if(direction == Vector2.zero && (_canPlayerMove == true && _canPlayerRotate == true))
        {
            PlayerState = PlayerState.Idle;
        }
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
        if (!_canPlayerRotate)
            return;
        
        if (rotationDirection.sqrMagnitude < 0.01f)
            return;
        
        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, movementStats.RotationSmoothTime * Time.fixedDeltaTime));
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
        if(_player.GetUnitClass().ClassAbilityList.AbilityDictionary[abilityEnum].IsOnCooldown() == true)
            return;
        
        _player.GetUnitClass().ClassAbilityList.AbilityDictionary[abilityEnum].OnTriggerAbility(gameObject, _abilityParameterHandler);
        PlayerState = _playerAbilityState[GetAbilityNumber(abilityEnum)];
    }

    private int GetAbilityNumber(AbilityExtendableEnum abilityEnum)
    {
        return _playerInput.GetabilityDictionary().AbilityOrderDictionary.
            FirstOrDefault(x => x.Value == abilityEnum).Key;
    }

    public void DisableMovement(bool canMove)
    {
        _canPlayerMove = canMove;
        _canPlayerRotate = canMove;
    }

    public void InitializePlayer(UnitClass unitClass, AbilityAnimationDictionary abilityAnimationDictionary)
    {
        _player.InitializeUnitClass(unitClass);
        _player.GetUnitClass().ClassAbilityList.InitializeAbilities();
        _abilityAnimationDictionary = abilityAnimationDictionary;
        SetupStateMachine();
    }

    public void ChangeInputReaderAbilityDictionary(AbilityDictionary newDictionary)
    {
        _playerInput.ChangeAbilityDictionary(newDictionary);
    }

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    public AbilityList GetAbilityList()
    {
        return _player.GetUnitClass().ClassAbilityList;
    }

    private void RayCastCheckForInteractables()
    {
        RaycastHit hitData;
        Debug.DrawRay(transform.position, transform.forward * _interactDistance.Value, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hitData, _interactDistance.Value, 
                _interactableLayerMask))
        {
            if (hitData.transform.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                _currentInteractable = interactable;
            }
        }
        else
        {
            _currentInteractable = null;
        }
    }

    private void OnInteract()
    {
        RayCastCheckForInteractables();
        _currentInteractable?.Interact();
    }

    public AbilityAnimationDictionary GetAbiliyAnimationDictionary()
    {
        return _abilityAnimationDictionary;
    }
}