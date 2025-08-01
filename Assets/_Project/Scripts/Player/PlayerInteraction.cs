using UnityEngine;

public class PlayerInteraction : MonoExt
{
    [SerializeField] private float _interactRange = 3f;
    [SerializeField] private KeyCode _interactKey = KeyCode.F;
    
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
    
    private void Update()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactRange))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * _interactRange);
    }
}
