using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectRange", menuName = "ScriptableObjects/Player/CollectRange")] [InlineEditor]
public class CollectableRangeScriptableObject : SerializedScriptableObject
{
    public float CollectableRange;
}
