using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Breakable Consequence", menuName = "ScriptableObjects/Ability/Damage Breakable Consequence")]
public class DamageBreakableConsequence : Consequence
{
    public Stat Damage;
    
    public AbilityParameterExtendableEnum EnemyListParameterKey;
    
    public override async UniTask ExecuteConsequence(AbilityParameterHandler abilityParameters)
    {
        List<GameObject> targetList = abilityParameters.GetParameter<List<GameObject>>(EnemyListParameterKey);
        if (targetList.Count <= 0)
        {
            abilityParameters.RemoveParameter(EnemyListParameterKey);
            return;
        }

        foreach (GameObject target in targetList.ToList())
        {
            if (!target.TryGetComponent<Breakable>(out var targetHealth))
            {
                targetList.Remove(target);
                continue;
            }
            targetHealth.ApplyDamage(Damage.Value);
        }
    }
}
