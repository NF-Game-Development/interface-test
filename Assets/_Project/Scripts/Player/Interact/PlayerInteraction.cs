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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _playerInteractRange.InteractRadiusRange);
        IInteractable nearest = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent<IInteractable>(out var interactable))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearest = interactable;
                    nearestDistance = distance;
                }
            }
        }

        nearest?.Interact();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _playerInteractRange.InteractRadiusRange);
    }
}
