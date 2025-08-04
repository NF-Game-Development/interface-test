using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "Collectable Animation", menuName = "ScriptableObjects/Interactable/Collectable Animation")] [InlineEditor]
public class CollectAnimationCurve : SerializedScriptableObject
{
    public AnimationCurve MoveCurve;
    public AnimationCurve ScaleCurve;
    public float Duration = 1f;
}
