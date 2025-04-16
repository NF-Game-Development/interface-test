using UnityEngine;

public class Health_Objects : Health
{
    [SerializeField] private IBreakable _breakable;
    private void Start()
    {
        //Events
        OnSubscriptionSet();
    }

    public override void OnDeath()
    {
        _breakable.Break();
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
