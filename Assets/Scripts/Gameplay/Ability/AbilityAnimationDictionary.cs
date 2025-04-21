using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "AbilityAnimationDictionary", menuName = "ScriptableObjects/Ability/AbilityAnimationDictionary")]
public class AbilityAnimationDictionary : SerializedScriptableObject
{
    public Dictionary<int, string> AnimationDictionary = new Dictionary<int, string>();
}
