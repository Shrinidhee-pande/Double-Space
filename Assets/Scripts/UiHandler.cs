using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UiHandler : MonoBehaviour
{
    public RectTransform[] panelList;

    private void Update()
    {
        if(GameManager.CurrentState == GameState.Play)
        {
            panelList[0].gameObject.SetActive(true);
            panelList[1].gameObject.SetActive(false);
        }
        else if(GameManager.CurrentState == GameState.GameOver)
        {
            panelList[0].gameObject.SetActive(false);
            panelList[1].gameObject.SetActive(true);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void StartLobby()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
