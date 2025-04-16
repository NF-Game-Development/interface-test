using TreeEditor;
using UnityEngine;

public interface ICollectable
{
    public void Collect(PlayerItemCollectedStats playerItemCollectedStats);
    public void MoveTowardsPlayer(Transform target);
}
