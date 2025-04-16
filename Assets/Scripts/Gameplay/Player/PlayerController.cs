using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoExt, IMovable, IRotatable, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private MovementStats _movementStats;
    [TabGroup("References")] [SerializeField] private PlayerInputReader _playerInput;
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
    
    
    private Vector2 _movementInput = Vector2.zero;
    private IAbilityCastable _abilityCastableImplementation;

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
        _playerInput.EnablePlayerActions();
        _abilityParameterHandler.Initialize();
    }
     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //Event that handles player movement
        AddEvent(_playerInput.Movement,movementDirection => _movementInput = movementDirection);
        AddEvent(_playerInput.Ability, OnAbilityCast);
        AddEvent(_abilityParameterHandler.AbilityCasted, DisableMovement);
        AddEvent(_abilityParameterHandler.AbilityEnded, DisableMovement);
        AddEvent(_playerInput.Interact, _ => OnInteract());
    }

    public void FixedUpdate()
    {
        HandleMovement();
        //RayCastCheckForInteractables();
    }

    //Handles movement and rotation
    private void HandleMovement()
    {
        if (!_canPlayerMove)
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
        if (!_canPlayerRotate)
            return;
        
        if (rotationDirection.sqrMagnitude < 0.01f)
            return;
        
        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, movementStats.RotationSmoothTime * Time.fixedDeltaTime));
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
        _player.GetUnitClass().ClassAbilityList.AbilityDictionary[abilityEnum].OnTriggerAbility(gameObject, _abilityParameterHandler);
    }

    public void DisableMovement(bool canMove)
    {
        _canPlayerMove = canMove;
        _canPlayerRotate = canMove;
    }

    public void InitializePlayer(UnitClass unitClass)
    {
        _player.InitializeUnitClass(unitClass);
        _player.GetUnitClass().ClassAbilityList.InitializeAbilities();
    }

    public void ChangeInputReaderAbilityDictionary(AbilityDictionary newDictionary)
    {
        _playerInput.ChangeAbilityDictionary(newDictionary);
    }

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    public void ChangePlayerModel()
    {
        _player.ChnageModel();
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
        _currentInteractable.Interact();
    }
}