using UnityEngine;

public interface IVisualiser
{
    public void SpawnVisualizer(Vector3 center, float radius, Vector3 halfExtents, Quaternion boxRotation);
}
