using UnityEngine;
using UnityEngine.Serialization;

public class Coin : MonoExt, ICollectable
{
    [FormerlySerializedAs("coinAmountGain")] [SerializeField] private int _coinAmountGain;
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

    public void Collect(PlayerItemCollectedStats playerItemCollectedStats)
    {
        playerItemCollectedStats.AddCoinsCollected(_coinAmountGain);
        Destroy(this.gameObject);
    }
}
