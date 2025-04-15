using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class Spawner : MonoExt
{
    public Camera Cam;
    public Transform PlayerSpawnPoint;
    public Transform EnemySpawnPoint;

    public ClassType PlayerToSpawn;
    public EnemyType EnemyToSpawn;

    
    public Dictionary<ClassType, GameObject> PlayerClassDictionary;
    public Dictionary<EnemyType, GameObject> EnemyTypeDictionary;

    
    public GameObject Player;
    public GameObject Enemy;

    private void Start()
    {
        SpawnPlayer();
    }


    [Button]
    public void SpawnPlayer()
    {
        if (Player == null)
        {
            GameObject player = Instantiate(PlayerClassDictionary[PlayerToSpawn], PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
            Cam.GetComponent<CameraController>().SetTransformTarget(player.transform);
            player.GetComponent<PlayerController>().SetCamera(Cam);
            Player = player;
        }

        if (Player != null)
        {
            Destroy(Player);
            GameObject player = Instantiate(PlayerClassDictionary[PlayerToSpawn], PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
            Cam.GetComponent<CameraController>().SetTransformTarget(player.transform);
            player.GetComponent<PlayerController>().SetCamera(Cam);
            Player = player;
            
        }
        
    }
    
    [Button]
    public void SpawnEnemy()
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
}
