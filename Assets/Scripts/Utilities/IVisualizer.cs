using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IVisualizer
{
    public void SpawnVisualizer( Vector3 center, Vector3 halfExtents, Quaternion boxRotation, float radius);
}