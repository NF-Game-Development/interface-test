using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deal Buff Consequence", menuName = "ScriptableObjects/Ability/Deal Buff Consequence")]
public class DealBuffConsequence : Consequence
{
    public Stat Buff;

    public AbilityParameterExtendableEnum HeroListParameterKey;
    
    public override async UniTask ExecuteConsequence(AbilityParameterHandler abilityParameters)
    {
        List<GameObject> targetList = abilityParameters.GetParameter<List<GameObject>>(HeroListParameterKey);
        if (targetList.Count <= 0)
        {
            abilityParameters.RemoveParameter(HeroListParameterKey);
            return;
        }

        foreach (GameObject target in targetList.ToList())
        {
            if (!target.transform.root.TryGetComponent<IHealable>(out var targetHealth))
            {
                targetList.Remove(target);
                continue;
            }
            targetHealth.ApplyHeal(Buff.Value);
        }
    }
}
