using UnityEngine;
using NF.Main.Core;
using NF.Main.Core.PlayerStateMachine;

public class PlayerAnimation : MonoExt
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;


    private PlayerInputReader _playerInputReader;
    private bool _canPlayerMove;
    private bool _canPlayerRotate;
    private StateMachine _stateMachine;
    
    public PlayerState PlayerState { get; set; }
    
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
        
        Any(idleState, new FuncPredicate(ReturnToIdleState));
        Any(moveState, new FuncPredicate(() => PlayerState == PlayerState.Moving));
        
        _stateMachine.SetState(idleState);
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
}
