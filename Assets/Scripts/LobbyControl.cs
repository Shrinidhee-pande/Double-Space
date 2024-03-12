using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyControl : MonoBehaviour
{
    private Lobby currentLobby;
    private float heartbeatCounter;

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In As: " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        ListLobbies();
    }
    private async void Update()
    {
        await HeartbeatHandler();
    }

    private async Task HeartbeatHandler()
    {
        if (currentLobby != null)
        {
            heartbeatCounter -= Time.deltaTime;

            if (heartbeatCounter < 0f)
            {
                heartbeatCounter += 25f;
                await LobbyService.Instance.SendHeartbeatPingAsync(currentLobby.Id);
            }
        }
    }

    public async void CreateLobbyByName()
    {
        LobbyCredentials lobbyCred = GetComponent<LobbyCredentials>();
        string name = lobbyCred.lobbyName;
        bool privateLobby = lobbyCred.IsPrivate;
        try
        {
            CreateLobbyOptions options = new CreateLobbyOptions
            {
                IsPrivate = privateLobby,
                Player = new Player
                {
                    Data = new Dictionary<string, PlayerDataObject> {
                        { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "name")}
                    }
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(name, 2, options);
            Debug.Log("Lobby created: " + lobby.Id + " " + lobby.Name);
            LobbyUi.Instance.CreatePlayerText(lobby);
            currentLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void ListLobbies()
    {
        try
        {
            QueryResponse response = await Lobbies.Instance.QueryLobbiesAsync();
            foreach (Lobby lobby in response.Results)
            {
                LobbyUi.Instance.CreateLobbyText(lobby);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinLobbyUsingCode()
    {
        LobbyCredentials lobbyCred = GetComponent<LobbyCredentials>();
        string code = lobbyCred.lobbyCode;

        try
        {
            Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code);
            Debug.Log("Lobby joined: " + lobby.Id + " " + lobby.Name);
            LobbyUi.Instance.CreatePlayerText(lobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

}
