using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;

    private int enemiesToSpawn;

    public override void OnNetworkSpawn()
    {
        //enemiesToSpawn = GameMode.Instance.enemies;
        if (IsHost)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[0].position, Quaternion.identity);
            enemy.GetComponent<NetworkObject>().Spawn();
        }
    }
    void Update()
    {
        
    }
}
