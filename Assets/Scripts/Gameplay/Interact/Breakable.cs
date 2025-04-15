using UnityEngine;
using UnityEngine.Serialization;

public class Breakable : MonoExt, IInteractable
{
    [SerializeField] private ItemDropList _itemDrops;
    [SerializeField] private float _dropChance;

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

    public void Interact()
    {
        DropItem();
        Destroy(this.gameObject);
    }

    private void DropItem()
    {
        if (_itemDrops.ItemDrops.Count == 0) return;

        float roll = Random.value;
        if (roll <= _dropChance)
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
    
}
