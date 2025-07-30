using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName="ClassName", menuName = "ScriptableObjects/Class/ClassEnum")] [InlineEditor]
public class ClassNameExtendableEnum : SerializedScriptableObject
{
    public string Id;
}
