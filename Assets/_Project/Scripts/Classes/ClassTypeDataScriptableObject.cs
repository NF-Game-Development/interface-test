using Sirenix.OdinInspector;
using UnityEngine;

public class ClassTypeDataScriptableObject : SerializedScriptableObject
{
    [TabGroup("Basic Info")] public ClassNameExtendableEnum ClassNameEnum;

    [TabGroup("Stats")] public float BaseHealth;
    [TabGroup("Stats")] public float BaseAttack;
    [TabGroup("Stats")] public float MeleeRadius;
    [TabGroup("Stats")] public MovementStats MovementStats;

    [TabGroup("Abilities")] public AbilityList AbilityList;
    [TabGroup("Abilities")] public AbilityParameterHandler AbilityParameterHandler;
}
