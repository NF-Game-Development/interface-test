using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BreakableObject : MonoExt, IBreakable
{
    [SerializeField] private bool _isDropLoot = true;
    [SerializeField] private GameObject _breakableObject;
    [SerializeField] private GameObject _breakEffect; // if there is time
    [SerializeField] private List<GameObject> _dropLootPrefabs; //Change Into scriptable object
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
        
        int indexOfLootToSpawn = Random.Range(0, _dropLootPrefabs.Count);
        
        Instantiate(_dropLootPrefabs[indexOfLootToSpawn], this.transform.position, Quaternion.identity);
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
