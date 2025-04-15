using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UniRx;
public class Breakable : MonoExt, IDamageable, IDropHandler
{
    [SerializeField] private Stat _maxHP;
    [SerializeField] private ItemDropList _itemDrops;
    [SerializeField] private Stat _dropChance;
    [SerializeField] private float _hp;

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
        _hp = _maxHP.Value;
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
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


    public void ApplyDamage(float damageValue)
    {
        _hp -= damageValue;

        if (_hp <= 0)
        {
            DropItem();
            Destroy(gameObject, 0.2f);
        }
    }
}
