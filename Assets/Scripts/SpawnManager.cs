using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;

    private int spawnedEnemies;
    private int enemiesToSpawn;

    public static int enemiesKilled;

    public override void OnNetworkSpawn()
    {
        enemiesToSpawn = GameMode.Instance.enemies;
        enemiesKilled = 0;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (IsHost)
        {
            int index = Random.Range(0, spawnPoints.Length);
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);
            enemy.GetComponent<NetworkObject>().Spawn();
            spawnedEnemies++;
        }
    }

    void Update()
    {
        if (spawnedEnemies == enemiesKilled)
        {
            int batch = Random.Range(5, 8);
            for (int i = 0; i < batch; i++)
            {
                SpawnEnemy();
            }
        
        }
    }
}
