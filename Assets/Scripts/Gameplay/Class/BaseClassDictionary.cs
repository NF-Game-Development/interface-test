using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassDictionary", menuName = "ScriptableObjects/UnitClass/ClassDictionary")]
public class BaseClassDictionary : SerializedScriptableObject
{
    public Dictionary<ClassEnum, UnitClass> ClassDictionary = new Dictionary<ClassEnum, UnitClass>();
}
