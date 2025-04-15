using UnityEngine;

public class Collectable : MonoExt, IInteractable
{
    [SerializeField] private PlayerInteraction _playerInteraction;
    [SerializeField] private ItemDropExtendableEnum _itemDropEnum;

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

    private void Collected()
    {
        if (_playerInteraction == null)
        {
            Debug.Log("This Collectable is too far away!");
            return;
        }
        
        _playerInteraction.AddCollectable(_itemDropEnum);
        Destroy(gameObject);
    }

    public void Interact()
    {
        Collected();
    }

    public void SetPlayerInteraction(PlayerInteraction playerInteraction)
    {
        _playerInteraction = playerInteraction;
    }
}
