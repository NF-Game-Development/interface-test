using UnityEngine;
using UnityEngine.Serialization;

public class Coin : Collectables
{
    [SerializeField] private CoinScriptableObject coinScriptableObject;
    
    private float _coinValue;

    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        
        _coinValue = coinScriptableObject.CoinValue;
    }
    
    public override void Collect()
    {
        Debug.Log($"Collected a coin worth {_coinValue}!");

        // TODO: Add to player's currency system
        // Example: PlayerCurrency.AddCoins(_coinValue);

        Destroy(gameObject);
    }
}
