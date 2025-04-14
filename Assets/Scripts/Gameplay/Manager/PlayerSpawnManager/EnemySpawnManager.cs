using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawnManger : MonoExt
{
    //[SerializeField] private EnemyUnit _enemyUnitPrefab;
    [SerializeField] private Transform _enemySpawnPoint;
    
    [FormerlySerializedAs("_baseClasDictionary")] [SerializeField] private BaseClassDictionary baseClassDictionary;
    [SerializeField] private Dictionary<ClassEnum, EnemyUnit> _enemyUnitPrefabs;
    
    private void Awake()
    {
        //Initialize mono extension
        Initialize();
    }
    private void Start()
    {
        //Events
        OnSubscriptionSet();
    }
    
    public override void Initialize()
    {
        base.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }

    [Button]
    public void SpawnEnemy(ClassEnum classEnum)
    {
        EnemyUnit newEnemyUnit = Instantiate(_enemyUnitPrefabs[classEnum], _enemySpawnPoint.position, Quaternion.identity);
        newEnemyUnit.InitializeUnitClass(baseClassDictionary.ClassDictionary[classEnum]);
    }
}
