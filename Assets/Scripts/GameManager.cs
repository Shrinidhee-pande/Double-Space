using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Play, GameOver
};
public class GameManager : MonoBehaviour
{
    public static GameState CurrentState { get; set; }
    public Canvas canvas;
    void Start()
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
