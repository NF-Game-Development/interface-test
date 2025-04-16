using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BreakableObject : MonoExt, IBreakable
{
    [SerializeField] private bool _isDropLoot = true;
    [SerializeField] private GameObject _breakableObject;
    [SerializeField] private GameObject _breakEffect; // if there is time
    [SerializeField] private LootDropList _dropLootPrefabs; //Change Into scriptable object
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

    private void SpawnLoot()
    {
        if(_isDropLoot == false)
            return;
        
        int amountOfLoot = Random.Range(1, 3); //change to scriptable
        int indexOfLootToSpawn;
        for (int index = 0; index < amountOfLoot; index++)
        {
            indexOfLootToSpawn = Random.Range(0, _dropLootPrefabs.LootDropPrefabs.Count);
            GameObject newLoot = Instantiate(_dropLootPrefabs.LootDropPrefabs[indexOfLootToSpawn], this.transform.position, 
                Quaternion.identity);

            if (newLoot.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                collectable.OnSpawnItemJumpUp();
            }
        }
    }

    private void BreakObject()
    {
        //PlayAnimation, but disable model for now
        _breakableObject?.SetActive(false);
        _breakEffect?.SetActive(true);
    }

    [Button]
    public void Break()
    {
        SpawnLoot();
        BreakObject();
    }
    
}
