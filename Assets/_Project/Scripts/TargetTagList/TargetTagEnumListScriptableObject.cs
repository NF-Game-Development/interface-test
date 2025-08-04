using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName="Target List", menuName = "ScriptableObjects/Utility/Target List")] [InlineEditor()]
public class TargetTagEnumListScriptableObject : SerializedScriptableObject
{ 
    public AbilityParameterExtendableEnum[] TargetTagEnumList;
}
