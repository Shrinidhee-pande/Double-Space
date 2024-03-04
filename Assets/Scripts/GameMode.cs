using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode Instance;
    public string objectiveDescription;
    public float timeToComplete;
    public float enemies;
    public float enemiesKilled;
    public bool ObjectiveMet { get; set; } = false;
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Update()
    {
        if (enemies == enemiesKilled)
        {
            ObjectiveMet = true;
            GameManager.CurrentState = GameState.GameOver;
        }
    }
}
