using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "AbilityDictionary", menuName = "ScriptableObjects/Ability/AbilityDictionary")]
public class AbilityDictionary : SerializedScriptableObject
{
    public Dictionary<int, AbilityExtendableEnum> AbilityOrderDictionary = new Dictionary<int, AbilityExtendableEnum>();

    [Button]
    private void InitializeAbilityDictionary(AbilityList abilityList)
    {
        AbilityOrderDictionary = new Dictionary<int, AbilityExtendableEnum>();
        List<AbilityExtendableEnum> abilityEnumList = abilityList.AbilityDictionary.Keys.ToList();
        for (int i = 0; i < abilityList.AbilityDictionary.Count; i++)
        {
            AbilityOrderDictionary.Add(i + 1, abilityEnumList[i]);
        }
    }
}
