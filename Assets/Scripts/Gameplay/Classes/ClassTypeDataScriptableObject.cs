using Sirenix.OdinInspector;
using UnityEngine;

public class ClassTypeDataScriptableObject : SerializedScriptableObject
{
    [TabGroup("Basic Info")]
    [LabelText("Class Name")]
    public ClassNameExtendableEnum ClassNameEnum;

    [TabGroup("Stats")]
    [LabelText("Health")]
    public int BaseHealth;

    [TabGroup("Stats")]
    [LabelText("Attack")]
    public int BaseAttack;

    [TabGroup("Stats")]
    [LabelText("Movement Speed")]
    public int BaseMovementSpeed;

    [TabGroup("Abilities")]
    [LabelText("Ability List")]
    public AbilityList Abilities;
}
