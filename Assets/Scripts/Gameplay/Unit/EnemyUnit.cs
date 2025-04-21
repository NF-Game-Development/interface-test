using System.Collections.Generic;
using _Project.Script.Core.EnemyStateMachine;
using NF.Main.Core;
using UnityEngine;

public class EnemyUnit : BaseUnit
{
    private StateMachine _stateMachine;
    [SerializeField] private Animator _animator;
    public EnemyState EnemyState;
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
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        AddEvent(Health.OnDeathEvent, _ => EnemyDeath());
    }

    private void EnemyDeath()
    {
        Debug.Log("enemy death");
        EnemyState = EnemyState.Death;
        Debug.Log(EnemyState);
    }
    
    public void SetupStateMachine()
    {
        // State Machine
        _stateMachine = new StateMachine();
            
        // Declare Player States
        var idleState = new EnemyIdleState(_animator);
        var deathState = new EnemyDeathState(_animator);
            
        // Define Player State Transitions
        Any(idleState, new FuncPredicate(ReturnToIdleState));
        Any(deathState, new FuncPredicate(() => EnemyState == EnemyState.Death));

        // Set Initial State
        _stateMachine.SetState(idleState);
    }
    
    private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    
    private bool ReturnToIdleState()
    {
        return EnemyState == EnemyState.Idle;
    }
}
