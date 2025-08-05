using System.Collections.Generic;
using UnityEngine;
using NF.Main.Core;
using NF.Main.Core.PlayerStateMachine;
using Sirenix.OdinInspector;

public class PlayerAnimation : MonoExt
{
    [TabGroup("References")] [SerializeField] private Animator _animator;
    [TabGroup("References")] [SerializeField] private PlayerController _playerController;

    [TabGroup("Ability Animations Data")] public AbilityAnimationDictionary _abilityAnimationDictionary;
    
    private PlayerInputReader _playerInputReader;
    private bool _canPlayerMove;
    private bool _canPlayerRotate;
    
    private StateMachine _stateMachine;
    
    public PlayerState PlayerState { get; private set; }
    private Dictionary<int, PlayerState> _playerAbilityState = new Dictionary<int, PlayerState>();
    
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

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public override void Initialize()
    {
        base.Initialize();

        InitializePlayerController();

        SetupStateMachine();
    }

    private void InitializePlayerController()
    {
        _playerInputReader = _playerController.PlayerInput;
        _canPlayerMove = _playerController.CanPlayerMove;
        _canPlayerRotate = _playerController.CanPlayerRotate;
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        
        AddEvent(_playerInputReader.Movement, TransitionToMoveState);
    }

    private void SetupStateMachine()
    {
        _stateMachine = new StateMachine();

        var idleState = new PlayerIdleState(_playerController, _animator);
        var moveState = new PlayerMoveState(_playerController, _animator);
        var basicAttackState = new PlayerBasicAttackState(_playerController, _animator);
        var ability1State = new PlayerAbility1State(_playerController, _animator);
        
        Debug.Log("Setup STATE machine "+ ability1State);
        
        Any(idleState, new FuncPredicate(ReturnToIdleState));
        Any(moveState, new FuncPredicate(() => PlayerState == PlayerState.Moving));
        Any(basicAttackState, new FuncPredicate(() => PlayerState == PlayerState.BasicAttack));
        Any(ability1State, new FuncPredicate(() => PlayerState == PlayerState.Ability1));
        
        Debug.Log("Setup STATE machine "+ ability1State);
        
        _playerAbilityState.Add(1, PlayerState.Ability1);
        
        _stateMachine.SetState(idleState);
    }

    public void SetState(PlayerState newState)
    {
        PlayerState = newState;
    }
    
    private bool ReturnToIdleState()
    {
        return PlayerState == PlayerState.Idle;
    }
    
    private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

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
    
    public void PlayAbilityAnimation(AbilityExtendableEnum abilityEnum)
    {
        if (_abilityAnimationDictionary.TryGetAnimationClipFromAbility(abilityEnum, out AnimationClip clip))
        {
            int animationHash = Animator.StringToHash(clip.name);

            if (_playerAbilityState.TryGetValue(abilityEnum.SkillNumber, out PlayerState state))
            {
                SetState(state); // triggers state machine change
                _animator.CrossFade(animationHash, 0.2f);
            }
        }
    }
}
