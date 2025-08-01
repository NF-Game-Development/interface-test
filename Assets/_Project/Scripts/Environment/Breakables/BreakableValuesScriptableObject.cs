using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Breakable", menuName = "ScriptableObjects/Interactable/Breakable")] [InlineEditor]
public class BreakableValuesScriptableObject : SerializedScriptableObject
{
    public float ObjectHealth;
}
