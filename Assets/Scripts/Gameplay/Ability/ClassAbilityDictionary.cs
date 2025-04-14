using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ClassAbilityDictionary", menuName = "ScriptableObjects/Ability/ClassAbilityDictionary")]
public class ClassAbilityDictionary : SerializedScriptableObject
{
    public Dictionary<ClassEnum, AbilityDictionary> AbilityOrderDictionary = new Dictionary<ClassEnum, AbilityDictionary>();
}
