using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyController : MonoExt, IMovable, IRotatable, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private MovementStats _movementStats;

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
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }

    public void Move(Vector3 movementDirection, MovementStats movementStats)
    {
        // put enemy movement code here
        Debug.Log("Enemy Movement");
    }

    public void Rotate(Vector3 rotationDirection, MovementStats movementStats)
    {
        // put enemy rotate here
        Debug.Log("Enemy Rotate");
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
        // put code for enemy doing ability here
        Debug.Log("Enemy Ability Cast");
    }
}
