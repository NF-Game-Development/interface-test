using UnityEngine;

public class BaseUnit : MonoExt
{
    protected UnitClass _unitClass;
    [SerializeField] protected Health Health;
    
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
        _unitClass = newUnitClass;
        Health.SetMaxHealth(_unitClass.ClassMaxHp);
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
