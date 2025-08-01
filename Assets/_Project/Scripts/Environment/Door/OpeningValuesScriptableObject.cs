using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Opening", menuName = "ScriptableObjects/Interactable/Opening")]
public class OpeningValuesScriptableObject : SerializedScriptableObject
{
    public float OpenAngle;
    public float Duration;
    public bool IsOpen;
}
