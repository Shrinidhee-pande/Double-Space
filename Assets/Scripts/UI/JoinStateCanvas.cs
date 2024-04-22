using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class JoinStateCanvas : CanvasScript
{
    [SerializeField] private Button click;
    
    void Start()
    {
        click.onClick.AddListener(ShowNextCanvas);
        click.interactable = false;
        NetworkManager.Singleton.OnClientConnectedCallback += ClientConnected;

    }

    private void ClientConnected(ulong obj)
    {
        click.GetComponentInChildren<TextMeshProUGUI>().text = "Click to start";
        click.interactable = true;
    }

    public override void ShowNextCanvas()
    {
        GameManager.Instance.currentState = GameState.Play;
        base.ShowNextCanvas();
    }
}
