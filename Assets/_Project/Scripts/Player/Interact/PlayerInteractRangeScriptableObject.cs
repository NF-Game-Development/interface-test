using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InteractRange", menuName = "ScriptableObjects/Player/InteractRange")]
public class PlayerInteractRangeScriptableObject : SerializedScriptableObject
{
    public float InteractRadiusRange;
}
