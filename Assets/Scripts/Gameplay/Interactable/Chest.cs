using Cysharp.Threading.Tasks;
using UnityEngine;

public class Chest : MonoExt, IInteractable
{
    [SerializeField] private LootDropList _lootDropPrefabs;
    [SerializeField] private bool _isInteractable = true;
    [SerializeField] private float _openChestTime;
    [SerializeField] private float _openChestFinalRotation = -120;
    [SerializeField] private GameObject _chestLid;
    [SerializeField] private Transform _lootSpawnTransform;
    [SerializeField] private LootDropAmountRange _lootDropAmountRange;
    
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
    
    private async UniTask OpenDoorAnimation()
    {
        _isInteractable = false;
        float remainingTime = 0;
        float rotationAlpha;
        float currentRotation;
        while (remainingTime < _openChestTime)
        {
            rotationAlpha = remainingTime / _openChestTime;
            await UniTask.Yield(PlayerLoopTiming.Update);
            currentRotation = Mathf.SmoothStep(0f, _openChestFinalRotation, rotationAlpha);
            _chestLid.transform.localRotation = Quaternion.Euler(currentRotation, 0, 0);
            remainingTime += Time.deltaTime;
        } 
        ChestLootDrop();
    }

    private void ChestLootDrop()
    {
        int amountOfLoot = Random.Range(_lootDropAmountRange.MinAmountLootDrop, _lootDropAmountRange.MaxAmountLootDrop);
        int indexOfLootToSpawn;
        for (int index = 0; index < amountOfLoot; index++)
        {
            indexOfLootToSpawn = Random.Range(0, _lootDropPrefabs.LootDropPrefabs.Count);
            BaseCollectable newLoot = Instantiate(_lootDropPrefabs.LootDropPrefabs[indexOfLootToSpawn], 
                _lootSpawnTransform.position, Quaternion.identity);
                newLoot.OnSpawnItemJumpUp();
        }
    }

    public void Interact()
    {
        if(_isInteractable == false)
            return;
        
        OpenDoorAnimation();
    }
}
