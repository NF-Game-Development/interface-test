using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
    [SerializeField] private Dictionary<UnitClass, GameObject> _playerModels = new Dictionary<UnitClass, GameObject>();
    [SerializeField] private PlayerItemCollectedStats _playerItemCollectedStats;
    [SerializeField] private Stat _collectableAttractionRadius;
    
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

    private void FixedUpdate()
    {
        AttractCollectables();
    }

    public void ChnageModel()
    {
        foreach (var model in _playerModels)
        {
            model.Value.SetActive(false);
        }
        
        _playerModels[_unitClass].SetActive(true);
    }
    
    public UnitClass GetUnitClass()
    {
        return _unitClass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollectable>(out ICollectable collectable))
        {
            collectable.Collect(_playerItemCollectedStats);
        }
    }

    private void AttractCollectables()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _collectableAttractionRadius.Value);
        
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                collectable.MoveTowardsPlayer(this.transform);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _collectableAttractionRadius.Value);
    }
}
