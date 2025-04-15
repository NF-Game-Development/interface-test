using UnityEngine;

public class Chest : MonoExt, IInteractable
{
    [SerializeField] private Vector3 _openPosition;
    [SerializeField] private Vector3 _openRotation;
    [SerializeField] private GameObject _chestLid;
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
        }
        else if (_isChestOpen)
        {
            _chestLid.transform.localPosition = Vector3.zero;
            _chestLid.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _isChestOpen = false;
        }
    }

    public void Interact()
    {
        OpenChest();
    }
}
