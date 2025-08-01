using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoExt
{
    [SerializeField] private PlayerInteractRangeScriptableObject _playerInteractRange;
    [SerializeField] private PlayerInteractKeyScriptableObject _playerInteractKeyScriptable;
    
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
        
        //_playerInteractRange.OnRangeChanged += newRange => Debug.Log("Interact radius changed to: " + newRange);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown((_playerInteractKeyScriptable.InteractKey)))
        {
            Interact();
        }
    }

    private void Interact()
    {
        float maxDistance = _playerInteractRange.InteractRadiusRange;
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.root.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
    
        Vector3 origin = transform.position + Vector3.up * 1f;
        Vector3 direction = transform.forward * _playerInteractRange.InteractRadiusRange;
    
        Gizmos.DrawRay(origin, direction);
    }
}
