using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCollectRange : MonoExt
{
    [SerializeField] private CollectableRangeScriptableObject _collectableRange;
    
    private float _autoCollectRange;
    
    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        
        InitializeCollectingRange();
    }

    private void InitializeCollectingRange()
    {
        _autoCollectRange = _collectableRange.CollectableRange;
    }

    private void Update()
    {
        CheckCollectablesInRange();
    }
    
    private void CheckCollectablesInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _autoCollectRange);

        foreach (var hit in hits)
        {
            if (hit.transform.root.TryGetComponent<ICollectible>(out var collectible))
            {
                collectible.Collect();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_collectableRange == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _autoCollectRange);
    }
    
    
}
