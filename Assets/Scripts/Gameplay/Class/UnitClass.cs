using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitClass", menuName = "ScriptableObjects/UnitClass/Class")]
public class UnitClass : SerializedScriptableObject
{
    public AbilityList ClassAbilityList;
    public Stat ClassMaxHp;
}
