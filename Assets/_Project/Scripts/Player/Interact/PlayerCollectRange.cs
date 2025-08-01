using UnityEngine;

public class PlayerCollectRange : MonoExt
{
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }
    
    
}
