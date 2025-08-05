using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementStats", menuName = "ScriptableObjects/Player/MovementStats")] [InlineEditor]
public class MovementStats : ScriptableObject
{
    public float MovementSpeed = 5f;
    public float RotationSmoothTime = 3f;
}