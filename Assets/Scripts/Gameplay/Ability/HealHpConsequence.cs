using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Consequence", menuName = "ScriptableObjects/Ability/Heal Consequence")]
public class HealHpConsequence : Consequence
{
    public Stat HealValue;
    
    public AbilityParameterExtendableEnum AllyHealParameterKey;
    
    public override async UniTask ExecuteConsequence(AbilityParameterHandler abilityParameters)
    {
        List<GameObject> targetList = abilityParameters.GetParameter<List<GameObject>>(AllyHealParameterKey);
        if (targetList.Count <= 0)
        {
            abilityParameters.RemoveParameter(AllyHealParameterKey);
            return;
        }

        foreach (GameObject target in targetList.ToList())
        {
            if (!target.TryGetComponent<HealableHealth>(out var targetHealth))
            {
                targetList.Remove(target);
                continue;
            }
            targetHealth.ApplyHeal(HealValue.Value);
        }
    }
}