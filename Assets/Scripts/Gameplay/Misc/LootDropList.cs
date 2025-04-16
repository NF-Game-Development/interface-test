using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "LootDropList", menuName = "ScriptableObjects/LootDropList")]
public class LootDropList : SerializedScriptableObject
{
    public List<GameObject> LootDropPrefabs = new List<GameObject>();
}
