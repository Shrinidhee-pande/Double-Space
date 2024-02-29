using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public enum GameState
{
    Play, GameOver
};
public class GameManager : MonoBehaviour
{
    public static GameState CurrentState { get; set; }
    public Canvas canvas;
    void Awake()
    {
        canvas.enabled = false;
        CurrentState = GameState.Play;
    }

    void Update()
    {
        if (CurrentState == GameState.GameOver)
        {
            canvas.enabled = true;
        }
    }

}
