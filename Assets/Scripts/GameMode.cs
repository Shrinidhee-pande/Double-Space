using UnityEngine;

public enum Mode
{
    Sabotage
}

public class GameMode : MonoBehaviour
{
    public static GameMode Instance;
    public float timeToComplete;
    public int enemies;
    public Mode mode;
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
        if (mode == Mode.Sabotage)
        {
            if (enemies <= SpawnManager.enemiesKilled)
            {
                GameMode.Instance.ObjectiveMet = true;
                GameManager.Instance.currentState = GameState.GameOver;
                SpawnManager.enemiesKilled = 0;
            }
        }
    }
}
