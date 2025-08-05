using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityAnimationDictionary", menuName = "ScriptableObjects/Ability/Ability Animation Dictionary")] [InlineEditor]
public class AbilityAnimationDictionary : SerializedScriptableObject
{
    [SerializeField][OdinSerialize] public Dictionary<AbilityExtendableEnum, AnimationClip> AnimationDictionary = new Dictionary<AbilityExtendableEnum, AnimationClip>();
    
    private Dictionary<AnimationClip, AbilityExtendableEnum> animationClipToAbilityMap;
    private Dictionary<int, AbilityExtendableEnum> animationHashToAbilityMap;

    private void OnEnable()
    {
        RebuildLookupTables();
    }
    
    public void RebuildLookupTables()
    {
        animationClipToAbilityMap = new Dictionary<AnimationClip, AbilityExtendableEnum>();
        animationHashToAbilityMap = new Dictionary<int, AbilityExtendableEnum>();

        var abilityKeys = new List<AbilityExtendableEnum>(AnimationDictionary.Keys);

        for (int i = 0; i < abilityKeys.Count; i++)
        {
            AbilityExtendableEnum abilityEnum = abilityKeys[i];
            AnimationClip animationClip = AnimationDictionary[abilityEnum];

            if (animationClip != null)
            {
                animationClipToAbilityMap[animationClip] = abilityEnum;

                int animationHash = Animator.StringToHash(animationClip.name);
                animationHashToAbilityMap[animationHash] = abilityEnum;
            }
        }
    }
    
    public bool TryGetAbilityEnumFromClip(AnimationClip clip, out AbilityExtendableEnum abilityEnum)
    {
        return animationClipToAbilityMap.TryGetValue(clip, out abilityEnum);
    }

    public bool TryGetAbilityEnumFromHash(int animationHash, out AbilityExtendableEnum abilityEnum)
    {
        return animationHashToAbilityMap.TryGetValue(animationHash, out abilityEnum);
    }

    public bool TryGetAnimationClipFromAbility(AbilityExtendableEnum abilityEnum, out AnimationClip animationClip)
    {
        return AnimationDictionary.TryGetValue(abilityEnum, out animationClip);
    }

    public bool TryGetAbilityEnumBySkillNumber(int skillNumber, out AbilityExtendableEnum abilityEnum)
    {
        foreach (AbilityExtendableEnum abilityExtendableEnum in AnimationDictionary.Keys)
        {
            if (abilityExtendableEnum != null && abilityExtendableEnum.SkillNumber == skillNumber)
            {
                abilityEnum = abilityExtendableEnum;
                return true;
            }
        }

        abilityEnum = null;
        return false;
    }
}
