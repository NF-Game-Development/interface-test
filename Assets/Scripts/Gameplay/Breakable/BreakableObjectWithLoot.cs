using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BreakableObjectWithLoot : BreakableObject
{
    [TabGroup("a", "Loot Data")]
    [SerializeField] private LootDropList _dropLootPrefabs;
    [TabGroup("a", "Loot Data")]
    [SerializeField] private LootDropAmountRange _lootDropAmountRange;
    [TabGroup("a", "Loot Data")]
    [SerializeField] private bool _isDropLoot = true;
    
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

    protected override void OnBreak()
    {
        Debug.Log("Fire Spawn loot");
        SpawnLoot();
        base.OnBreak();
    }

    private void SpawnLoot()
    {
        if(_isDropLoot == false)
            return;
        
        int amountOfLoot = Random.Range(_lootDropAmountRange.MinAmountLootDrop, _lootDropAmountRange.MaxAmountLootDrop);
        int indexOfLootToSpawn;
        for (int index = 0; index < amountOfLoot; index++)
        {
            indexOfLootToSpawn = Random.Range(0, _dropLootPrefabs.LootDropPrefabs.Count);
            BaseCollectable newLoot = Instantiate(_dropLootPrefabs.LootDropPrefabs[indexOfLootToSpawn], 
                this.transform.position, Quaternion.identity);
            newLoot.OnSpawnItemJumpUp();
        }
        
        Debug.Log("Spawn loot");
    }
    
}
