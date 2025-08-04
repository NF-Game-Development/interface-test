using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObjects/Stat")] [InlineEditor]
public class Stat : SerializedScriptableObject
{
    public float Value;
}