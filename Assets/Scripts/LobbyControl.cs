using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyControl : MonoBehaviour
{
    private Lobby createdLobby;
    private Lobby joinedLobby;
    private float heartbeatCounter;
    //private float lobbyUpdateCounter;
    private PlayerCredentials playerCreds;

    private async void Start()
    {
        playerCreds = GetComponent<PlayerCredentials>();
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In As: " + AuthenticationService.Instance.PlayerId);
            playerCreds.SetPlayerId(AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        ListLobbies();
    }
    private void Update()
    {
        HeartbeatHandler();
        //LobbyUpdateHandler();
    }

    private async void HeartbeatHandler()
    {
        if (createdLobby != null)
        {
            heartbeatCounter -= Time.deltaTime;

            if (heartbeatCounter < 0f)
            {
                heartbeatCounter += 25f;
                await LobbyService.Instance.SendHeartbeatPingAsync(createdLobby.Id);
            }
        }
    }
    /*
        private async void LobbyUpdateHandler()
        {
            if (createdLobby != null)
            {
                lobbyUpdateCounter -= Time.deltaTime;

                if (lobbyUpdateCounter < 0f)
                {
                    lobbyUpdateCounter += 25f;
                    Lobby lobby  = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
                    joinedLobby = lobby;
                }
            }
        }*/

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
                        { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerCreds.GetPlayerName())}
                    }
                }
            };
            LobbyEventCallbacks callbacks = new LobbyEventCallbacks();
            callbacks.PlayerJoined += LobbyUi.Instance.CreatePlayerText;

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(name, 2, options);
            Debug.Log("Lobby created: " + lobby.Id + " " + lobby.Name);


            LobbyUi.Instance.UpdateLobbyText(lobby);
            LobbyUi.Instance.CreatePlayerText(lobby);

            await LobbyService.Instance.SubscribeToLobbyEventsAsync(lobby.Id, callbacks);

            createdLobby = lobby;
            joinedLobby = lobby;
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

        JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions
        {
            Player = new Player(
                    data: new Dictionary<string, PlayerDataObject> {
                        { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerCreds.GetPlayerName())}
                    }
                )
        };

        LobbyEventCallbacks callbacks = new LobbyEventCallbacks();
        callbacks.PlayerJoined += LobbyUi.Instance.CreatePlayerText;

        try
        {
            Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code, options);
            Debug.Log("Lobby joined: " + lobby.Id + " " + lobby.Name);

            LobbyUi.Instance.UpdateLobbyText(lobby);
            LobbyUi.Instance.CreatePlayerText(lobby);

            await LobbyService.Instance.SubscribeToLobbyEventsAsync(lobby.Id, callbacks);

            joinedLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    /*public async void UpdatePlayer()
    {
        UpdatePlayerOptions options = new UpdatePlayerOptions
        {

        };
        try
        {
            await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, options);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }*/

    public void StartGame()
    {

    }

}
