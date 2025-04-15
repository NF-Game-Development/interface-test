using UnityEngine;
using System.Collections;

public class Collectable : MonoExt
{
    [SerializeField] private PlayerInteraction _playerInteraction;
    [SerializeField] private ItemDropExtendableEnum _itemDropEnum;
    
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _pickupDistance = 0.5f;
    private bool _isBeingCollected = false;

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

    public void Collect()
    {
        if (_playerInteraction == null)
        {
            Debug.Log("This Collectable is too far away!");
            return;
        }
        
        // _playerInteraction.AddCollectable(_itemDropEnum);
        // Destroy(gameObject);
        
        if (!_isBeingCollected)
        {
            StartCoroutine(MoveToPlayer());
        }
    }
    
    private IEnumerator MoveToPlayer()
    {
        _isBeingCollected = true;
        Transform target = _playerInteraction.transform;

        while (Vector3.Distance(transform.position, target.position) > _pickupDistance)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * _moveSpeed);
            yield return null;
        }

        _playerInteraction.AddCollectable(_itemDropEnum);
        Destroy(gameObject);
    }

    public void SetPlayerInteraction(PlayerInteraction playerInteraction)
    {
        _playerInteraction = playerInteraction;
    }
}
