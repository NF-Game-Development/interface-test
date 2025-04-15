using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Sphere Overlap Consequence", menuName = "ScriptableObjects/Ability/Sphere Overlap Consequence")]
public class SphereOverlapConsequence : Consequence, IVisualizer
{
    public Stat Radius;
    public Stat FrontOffset;
    
    public AbilityParameterExtendableEnum CenterParameterKey;
    public AbilityParameterExtendableEnum TargetListParameterKey;
    public List<AbilityParameterExtendableEnum> TargetTags;
    
    public bool IsVisualized = false;
    [ShowIf("IsVisualized")]
    public SphereOverlapConsequenceVisualizer VisualizerPrefab;
    
    public override async UniTask ExecuteConsequence(AbilityParameterHandler abilityParameters)
    {
        Transform centerTransform = abilityParameters.GetParameter<GameObject>(CenterParameterKey).transform;
        
        Vector3 center = centerTransform.position + centerTransform.forward * FrontOffset.Value;
        Quaternion boxRotation = Quaternion.LookRotation(centerTransform.forward);
        
        if(IsVisualized)
            SpawnVisualizer(center, Vector3.zero, boxRotation, Radius.Value);
        
        Collider[] colliders = Physics.OverlapSphere(center, Radius.Value);
        List<GameObject> targets = new();
        
        foreach (Collider collider in colliders)
        {
            foreach (AbilityParameterExtendableEnum tag in TargetTags)
            {
                if (!collider.CompareTag(tag.name))
                    continue;
            }
            
            targets.Add(collider.gameObject);
        }
        
        abilityParameters.SetParameter(TargetListParameterKey, targets.ToList());
        await ExecuteNextConsequence(abilityParameters);
    }
    
    public void SpawnVisualizer(Vector3 center, Vector3 halfExtents, Quaternion boxRotation, float radius)
    {
        SphereOverlapConsequenceVisualizer visualizer = Instantiate(VisualizerPrefab, center, boxRotation);
        visualizer.Initialize(center, radius, boxRotation);
        
    }
}