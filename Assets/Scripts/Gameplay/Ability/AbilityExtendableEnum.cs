using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName="Ability", menuName = "ScriptableObjects/Ability/AbilityEnum")]
public class AbilityExtendableEnum : SerializedScriptableObject
{
    public int SkillNumber;
    
    public override string ToString()
    {
        return $"{name} (Skill #{SkillNumber})";
    }
}
