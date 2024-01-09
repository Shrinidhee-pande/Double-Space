using UnityEngine;

public enum GameState
{
    Start,Play, GameOver
};
public class GameManager : MonoBehaviour
{
    public GameState CurrentState { get; set; }
    public static GameManager Instance;
    void Start()
    {
        CurrentState = GameState.Start;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

}
