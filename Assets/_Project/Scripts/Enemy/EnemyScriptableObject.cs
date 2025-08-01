using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/AddEnemy/Enemy")]
public class EnemyScriptableObject : ClassTypeDataScriptableObject
{
    [TabGroup("Model")] [LabelText("Hero Model Prefab")] public GameObject EnemyModelPrefab;
}
