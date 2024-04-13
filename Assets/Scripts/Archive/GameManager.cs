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
    void Awake()
    {
        CurrentState = GameState.Play;
    }
}
