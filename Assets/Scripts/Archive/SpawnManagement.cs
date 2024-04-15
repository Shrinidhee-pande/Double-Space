using UnityEngine;

public class SpawnManagement : MonoBehaviour
{
    public static int enemiesToSpawn;
    public GameObject[] enemyPrefabs;
    public Vector2 bounds;
    private int waveNumber;
    private void Start()
    {
        waveNumber = 1;
        enemiesToSpawn = waveNumber * 3;
        Spawn();
    }

    private void Update()
    {
        if (enemiesToSpawn == 0)
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        enemiesToSpawn = waveNumber * 2;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (GameManager.Instance.currentState == GameState.Play)
            {
                float x = Random.Range(-bounds.x, bounds.x);
                float y = Random.Range(-bounds.y, bounds.y);
                int index = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[index], new Vector2(x, y), Quaternion.identity);
            }
        }
        waveNumber++;
    }

}
