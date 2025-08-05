using UnityEngine;
using NF.Main.Core;
using NF.Main.Core.PlayerStateMachine;

public class PlayerAnimation : MonoExt
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;


    private PlayerInputReader _playerInputReader;
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
        _playerInputReader = _playerController._playerInput;
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        
        //AddEvent(_playerInputReader.Movement, );
    }

    private void SetupStateMachine()
    {
        _stateMachine = new StateMachine();

        var idleState = new PlayerIdleState(_playerController, _animator);
        
        Any(idleState, new FuncPredicate(ReturnToIdleState));
        
        _stateMachine.SetState(idleState);
    }
    
    private bool ReturnToIdleState()
    {
        return PlayerState == PlayerState.Idle;
    }
    
    private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
}
