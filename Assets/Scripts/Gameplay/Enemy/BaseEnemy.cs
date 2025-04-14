using Sirenix.OdinInspector;
using UnityEngine;

public class BaseEnemy : MonoExt
{
    [TabGroup("References")] [SerializeField]
    private EnemyType _enemyType;
    [TabGroup("References")] [SerializeField]
    private Health _health;
    private void Awake()
    {
        //Initialize mono extension
        Initialize();
        _health.MaxHP = _enemyType.MaxHealth.Value;
    }
    
    private void Start()
    {
        //Events
        OnSubscriptionSet();
        //_health.MaxHP = _enemyType.MaxHealth.Value;
    }
    
    public override void Initialize()
    {
        base.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }

    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }
}
