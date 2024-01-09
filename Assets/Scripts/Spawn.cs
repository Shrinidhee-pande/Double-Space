using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector2 bounds;

    private void Start()
    {
        InvokeRepeating(nameof(Spawns), 2, 5);
    }

    private void Spawns()
    {
        if(GameManager.Instance.CurrentState == GameState.Play)
        {
            int number = Random.Range(1, 7);
            for (int i = 0; i < number; i++)
            {
                float x = Random.Range(-bounds.x, bounds.x);
                float y = Random.Range(-bounds.y, bounds.y);

                Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity);
            }
        }
    }
}
