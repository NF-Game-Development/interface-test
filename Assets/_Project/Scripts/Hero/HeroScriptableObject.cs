using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName="NewHero", menuName = "ScriptableObjects/AddHero/Hero")][InlineEditor]
public class HeroScriptableObject : ClassTypeDataScriptableObject
{
    [TabGroup("Model")]
    [LabelText("Hero Model Prefab")]
    public GameObject HeroModelPrefab;
}
