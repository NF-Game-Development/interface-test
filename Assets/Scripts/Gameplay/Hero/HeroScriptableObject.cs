using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName="NewHero", menuName = "ScriptableObjects/AddHero/Hero")]
public class HeroScriptableObject : ClassTypeDataScriptableObject
{
    public GameObject HeroModelPrefab;
}
