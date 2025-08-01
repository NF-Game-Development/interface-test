using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Opening", menuName = "ScriptableObjects/Interactable/Opening")] [InlineEditor]
public class OpeningValuesScriptableObject : SerializedScriptableObject
{
    public float OpenAngleX;
    public float OpenAngleY;
    public float OpenAngleZ;
    public float Duration;
    public bool IsOpen;
}
