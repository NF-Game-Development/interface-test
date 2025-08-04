using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName="Target List", menuName = "ScriptableObjects/Utility/Target List")] [InlineEditor()]
public class TargetTagEnumListScriptableObject : SerializedScriptableObject
{ 
    public AbilityParameterExtendableEnum[] TargetTagEnumList;
    
    public bool IsMatchEnum(Collider collider)
    {
        foreach (var tagEnum in TargetTagEnumList)
        {
            if (collider.CompareTag(tagEnum.name))
                return true;
        }
        return false;
    }
}
