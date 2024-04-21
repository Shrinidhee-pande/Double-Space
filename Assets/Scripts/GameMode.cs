using UnityEngine;

public enum Mode
{
    Sabotage,
    Survival
}

public class GameMode : MonoBehaviour
{
    public static GameMode Instance;
    public string objectiveDescription;
    public float timeToComplete;
    public int enemies;
    public int enemiesKilled;
    public Mode mode;
    public bool ObjectiveMet { get; set; } = false;

    private float currentTime;
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        currentTime = timeToComplete + Time.deltaTime;
    }

    private void Update()
    {
        if (mode == Mode.Sabotage)
        {
            if (enemies == enemiesKilled)
            {
                ObjectiveMet = true;
                GameManager.Instance.currentState = GameState.GameOver;
            }
        }
        else if(mode == Mode.Survival)
        {
            currentTime--;
            if (currentTime < 0) {
                ObjectiveMet = true;
                GameManager.Instance.currentState = GameState.GameOver;
            }
        }
    }
}
