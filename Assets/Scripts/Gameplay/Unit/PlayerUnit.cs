using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
    [SerializeField] private Dictionary<UnitClass, GameObject> _playerModels = new Dictionary<UnitClass, GameObject>();
    [SerializeField] PlayerItemCollectedStats _playerItemCollectedStats;
    
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
}
