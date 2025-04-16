using NF.Main.Core.EnemyStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;
using NF.Main.Core;

public class BaseEnemy : MonoExt
{
    [TabGroup("References")] [SerializeField]
    private EnemyType _enemyType;
    [TabGroup("References")] [SerializeField]
    private Health _health;
    [TabGroup("References")] [SerializeField] private StateMachine _stateMachine;

    [TabGroup("References")] public EnemyState EnemyState;
    public Animator _animator;

    private void Awake()
    {
        //Initialize mono extension
        Initialize();
        _health.MaxHP = _enemyType.MaxHealth.Value;
        SetupStateMachine();
    }
    
    private void Start()
    {
        //Events
        OnSubscriptionSet();
        //_health.MaxHP = _enemyType.MaxHealth.Value;
        _health.DestroyOffset = 2f;
    }
    
    private void Update()
    {
        _stateMachine.Update();
    }
    
    public override void Initialize()
    {
        base.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        AddEvent(_health.OnDeath, _ => EnemyDead());
    }
    private void SetupStateMachine()
    {
        _stateMachine = new StateMachine();

        var idleState = new EnemyIdleState(this, _animator);
        var deadState = new EnemyDeathState(this, _animator);

        // Transitions
        Any(idleState, new FuncPredicate(ReturnToIdleState));
        Any(deadState, new FuncPredicate(IsDead));

        _stateMachine.SetState(idleState);
    }

    private void EnemyDead()
    {
        EnemyState = EnemyState.Dead;
    }

    private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

    private bool ReturnToIdleState() => EnemyState == EnemyState.Idle;
    private bool IsDead() => EnemyState == EnemyState.Dead;
    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }
}
