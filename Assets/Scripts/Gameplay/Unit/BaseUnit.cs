using UnityEngine;

public class BaseUnit : MonoExt
{
    public UnitClass UnitClass;
    [SerializeField] private Health Health;
    
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

    public void InitializeUnitClass(UnitClass newUnitClass)
    {
        UnitClass = newUnitClass;
        Health.SetMaxHealth(UnitClass.ClassMaxHp);
    }
    
    public override void Initialize()
    {
        base.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }    
}
