using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine.InputSystem;

public class Spawner : MonoExt
{
    public Subject<Unit> OnPlayerChanged;
    public Camera Cam;
    public Transform PlayerSpawnPoint;
    public Transform EnemySpawnPoint;
    
    public ClassType PlayerToSpawn;
    
    public Dictionary<ClassType, GameObject> PlayerClassDictionary;
    public Dictionary<EnemyType, GameObject> EnemyTypeDictionary;
    
    public GameObject Player;
    public GameObject Enemy;

    private void Awake()
    {
        OnPlayerChanged = new Subject<Unit>();
    }

    private void Start()
    {
        SpawnPlayer(PlayerToSpawn);
    }

    [Button]
    public void SpawnPlayer(ClassType PlayerToSpawn)
    {
        if (Player == null)
        {
            GameObject player = Instantiate(PlayerClassDictionary[PlayerToSpawn], PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
            Cam.GetComponent<CameraController>().SetTransformTarget(player.transform);
            InitializePlayerOnSpawn(player);
        }

        if (Player != null)
        {
            Destroy(Player);
            GameObject player = Instantiate(PlayerClassDictionary[PlayerToSpawn], PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
            Cam.GetComponent<CameraController>().SetTransformTarget(player.transform);
            InitializePlayerOnSpawn(player);
        }
        OnPlayerChanged.OnNext(Unit.Default);
    }
    
    [Button]
    public void SpawnEnemy(EnemyType EnemyToSpawn)
    {
        if (Enemy == null)
        {
            GameObject enemy = Instantiate(EnemyTypeDictionary[EnemyToSpawn], EnemySpawnPoint.position, EnemySpawnPoint.rotation);
            Enemy = enemy;
        }

        if (Enemy != null)
        {
            Destroy(Enemy);
            GameObject enemy = Instantiate(EnemyTypeDictionary[EnemyToSpawn], EnemySpawnPoint.position, EnemySpawnPoint.rotation);
            Enemy = enemy;
        }
        
    }

    private void InitializePlayerOnSpawn(GameObject player)
    {
        player.GetComponent<PlayerController>().SetCamera(Cam);
        player.GetComponent<PlayerInteraction>().SetCamera(Cam);
        Player = player;
    }
}
