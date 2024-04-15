using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyCanvas : CanvasScript
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button join;
    [SerializeField] private Button cancel;

    void Start()
    {
        input.onEndEdit.AddListener(LobbyData.Instance.SetLobbyCode);
        join.onClick.AddListener(ShowNextCanvas);
        cancel.onClick.AddListener(ShowPreviousCanvas);
    }
    private void Update()
    {
        if (string.IsNullOrEmpty(input.text))
        {
            join.interactable = false;
        }
        else
        {
            join.interactable = true;
        }
    }

    public override void ShowNextCanvas()
    {
        JoinLobbyUsingCode();
        base.ShowNextCanvas();
    }

    public async void JoinLobbyUsingCode()
    {
        JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions
        {
            Player = new Player(
                    data: new Dictionary<string, PlayerDataObject> {
                        { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, PlayerData.Instance.GetPlayerName())}
                    }
                )
        };
        
        try
        {
            Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(LobbyData.Instance.GetLobbyCode(), options);
            LobbyData.Instance.SetLobby(lobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
