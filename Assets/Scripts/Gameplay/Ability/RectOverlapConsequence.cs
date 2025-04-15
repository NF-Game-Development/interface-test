using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Rect Overlap Consequence", menuName = "ScriptableObjects/Ability/Rect Overlap Consequence")]
public class RectOverlapConsequence : Consequence, IVisualizer
{
    public Stat Width;
    public Stat Depth;
    public Stat Height;
    public Stat FrontOffset;

    public AbilityParameterExtendableEnum CenterParameterKey;
    public AbilityParameterExtendableEnum TargetListParameterKey;
    public List<AbilityParameterExtendableEnum> TargetTags;
    
    public bool IsVisualized = false;
    [ShowIf("IsVisualized")]
    public RectOverlapConsequenceVisualizer VisualizerPrefab;
    
    
    public override async UniTask ExecuteConsequence(AbilityParameterHandler abilityParameters)
    {
        Transform centerTransform = abilityParameters.GetParameter<GameObject>(CenterParameterKey).transform;
        
        Vector3 center = centerTransform.position + centerTransform.forward * FrontOffset.Value;
        Vector3 halfExtents = Utility.CalculateHalfExtents(Width.Value, Height.Value, Depth.Value);
        
        Quaternion boxRotation = Quaternion.LookRotation(centerTransform.transform.forward);
        
        if(IsVisualized)
            SpawnVisualizer(center, halfExtents, boxRotation,0f);
        
        Collider[] colliders = Physics.OverlapBox(center, halfExtents, boxRotation);
        List<GameObject> targets = new();
        
        foreach (Collider collider in colliders)
        {
            //refactored to work with list of tags
            bool hasMatchingTag = TargetTags.Any(tag => collider.CompareTag(tag.name));
            if (!hasMatchingTag) continue;

            targets.Add(collider.gameObject);
        }
        
        abilityParameters.SetParameter(TargetListParameterKey, targets.ToList());
        await ExecuteNextConsequence(abilityParameters);
    }
    
    public void SpawnVisualizer(Vector3 center, Vector3 halfExtents, Quaternion boxRotation, float radius)
    {
        RectOverlapConsequenceVisualizer visualizer = Instantiate(VisualizerPrefab, center, boxRotation);
        visualizer.Initialize(center, halfExtents, boxRotation);    }
}