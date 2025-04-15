using UnityEngine;
using UnityEngine.Serialization;
using UniRx;
public class Breakable : MonoExt, IDamageable
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

    private void DropItem()
    {
        if (_itemDrops.ItemDrops.Count == 0) return;

        float roll = Random.value;
        if (roll <= _dropChance.Value)
        {
            int index = Random.Range(0, _itemDrops.ItemDrops.Count);
            GameObject item = _itemDrops.ItemDrops[index];

            Instantiate(item, transform.position, Quaternion.identity);

            Debug.Log("Dropped item: " + item.name);
        }
        
        else
        {
            Debug.Log("No item dropped.");
        }
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
