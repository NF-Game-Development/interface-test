using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoExt, IInteractable
{
    [SerializeField] private Vector3 _openRotation;
    [SerializeField] private GameObject _door;
    private bool _isDoorOpen = false;
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

    private void OpenDoor()
    {
        if (!_isDoorOpen)
        {
            _door.transform.rotation = Quaternion.Euler(_openRotation);
            _isDoorOpen = true;
        }
        else if (_isDoorOpen)
        {
            _door.transform.rotation = Quaternion.Euler(0, 0, 0);
            _isDoorOpen = false;
        }
    }

    public void Interact()
    {
        OpenDoor();
    }
}
