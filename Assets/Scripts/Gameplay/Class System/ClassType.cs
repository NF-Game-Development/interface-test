using UnityEngine;

[CreateAssetMenu(fileName = "ClassType", menuName = "ScriptableObjects/ClassType")]
public class ClassType : ScriptableObject
{
    public string ID;
    public AbilityList AbilityList;
    public AbilityParameterHandler AbilityParameterHandler;
}
