using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassPrefabDictionary", menuName = "ScriptableObjects/UnitClass/ClassPrefabDictionary")]
public class ClassPrefabDictionary : SerializedScriptableObject
{
    public Dictionary<ClassEnum, PlayerController> PrefabDictionary = new Dictionary<ClassEnum, PlayerController>();
}
