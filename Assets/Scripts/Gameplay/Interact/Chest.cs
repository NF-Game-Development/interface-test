using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoExt, IInteractable, IDropHandler 
{
    [SerializeField] private Vector3 _openPosition;
    [SerializeField] private Vector3 _openRotation;
    [SerializeField] private GameObject _chestLid;
    [SerializeField] private ItemDropList _itemDrops;
    [SerializeField] private Stat _dropChance;
    private bool _isChestOpen = false;
    
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
    
    private void OpenChest()
    {
        if (!_isChestOpen)
        {
            _chestLid.transform.localPosition = _openPosition;
            _chestLid.transform.localRotation = Quaternion.Euler(_openRotation);
            _isChestOpen = true;
            DropItem();
        }
        else if (_isChestOpen)
        {
            _chestLid.transform.localPosition = Vector3.zero;
            _chestLid.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _isChestOpen = false;
        }
    }
    
    public void DropItem()
    {
        if (_itemDrops.ItemDrops.Count == 0) return;

        float roll = Random.value;
        if (roll <= _dropChance.Value)
        {
            GameObject selectedItem = GetRandomDrop(_itemDrops.ItemDrops);

            if (selectedItem != null)
            {
                Instantiate(selectedItem, transform.position, Quaternion.identity);
                Debug.Log("Dropped item: " + selectedItem.name);
            }
            else
            {
                Debug.LogWarning("Gacha roll failed. No item matched.");
            }
        }
        
        else
        {
            Debug.Log("No item dropped.");
        }
    }
    
    public GameObject GetRandomDrop(List<ItemDrop> drops)
    {
        float totalRate = 0f;
        foreach (var drop in drops)
        {
            totalRate += drop.ItemDropRate;
        }

        float roll = Random.Range(0f, totalRate);
        float cumulative = 0f;

        foreach (var drop in drops)
        {
            cumulative += drop.ItemDropRate;
            if (roll <= cumulative)
            {
                return drop.ItemPrefab;
            }
        }

        return null;
    }

    public void Interact()
    {
        OpenChest();
    }
}
