using UnityEngine;

public class ArmorLoot : BaseCollectable
{
    [SerializeField] private ArmorItemEnum _armorItemEnum;
    
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

    protected override void OnCollect(PlayerItemCollectedStats playerItemCollectedStats)
    {
        playerItemCollectedStats.AddArmorItem(_armorItemEnum);
        Destroy(this.gameObject);
    }
}
