using Sirenix.OdinInspector;
using UnityEngine;

public class ClassTypeDataScriptableObject : SerializedScriptableObject
{
    [TabGroup("Basic Info")] public ClassNameExtendableEnum ClassNameEnum;

    [TabGroup("Stats")] public int BaseHealth;
    [TabGroup("Stats")] public int BaseAttack;
    [TabGroup("Stats")] public MovementStats MovementStats;

    [TabGroup("Abilities")] public AbilityList AbilityList;
    [TabGroup("Abilities")] public AbilityParameterHandler AbilityParameterHandler;
}
