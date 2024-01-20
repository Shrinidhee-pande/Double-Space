using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIHandler : MonoBehaviour
{
    public Canvas[] canvases;

    void Update()
    {

        if (GameManager.Instance.CurrentState == GameState.Play)
        {
            canvases[0].enabled = false;
            canvases[1].enabled = false;
        }
        else if (GameManager.Instance.CurrentState == GameState.Start)
        {
            canvases[1].enabled = false;
            canvases[0].enabled = true;
        }
        else if (GameManager.Instance.CurrentState == GameState.GameOver)
        {
            canvases[1].enabled = true;
            canvases[0].enabled = false;
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit;
#endif
    }
    public void StartGame()
    {
        GameManager.Instance.CurrentState = GameState.Play;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.CurrentState = GameState.Play;
    }
}
