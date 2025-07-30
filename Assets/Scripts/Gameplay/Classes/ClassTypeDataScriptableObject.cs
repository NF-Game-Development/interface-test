using Sirenix.OdinInspector;
using UnityEngine;

public class ClassTypeDataScriptableObject : SerializedScriptableObject
{
    public ClassNameExtendableEnum ClassNameEnum;
    public int BaseHealth;
    public int BaseAttack;
    public int BaseMovementSpeed;
    public AbilityList Abilities;
}
