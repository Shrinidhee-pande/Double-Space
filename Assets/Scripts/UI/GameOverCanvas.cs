using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button back;
    
    void Start()
    {
        back.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.currentState = GameState.Lobby;
    }

    void Update()
    {
        if (GameManager.Instance.currentState == GameState.GameOver)
        {
            if (GameMode.Instance.ObjectiveMet == true)
            {
                text.text = "Misson\nComplete";
            }
            else
            {
                text.text = "Misson\nFailed";
            }
        }
    }
}
