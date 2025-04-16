using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "LootDropList", menuName = "ScriptableObjects/Loot/LootDropList")]
public class LootDropList : SerializedScriptableObject
{
    public List<BaseCollectable> LootDropPrefabs = new List<BaseCollectable>();
}
