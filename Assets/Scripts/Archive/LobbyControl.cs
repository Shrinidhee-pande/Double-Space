using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyControl : MonoBehaviour
{
    private Lobby createdLobby;
    private Lobby joinedLobby;

    private float heartbeatCounter;
    private float lobbyUpdateCounter;

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
        LobbyUpdateHandler();
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
    private async void LobbyUpdateHandler()
    {
        if (joinedLobby != null)
        {
            lobbyUpdateCounter -= Time.deltaTime;

            if (lobbyUpdateCounter < 0f)
            {
                lobbyUpdateCounter += 2f;
                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
                joinedLobby = lobby;

                if (lobby.Data["RelayCode"].Value != "0")
                {
                    if(!IsHost(lobby))
                    {
                        SceneManager.LoadScene(3);
                        RelayControl.Instance.JoinRelay(lobby.Data["RelayCode"].Value);
                    }
                }

            }
        }
    }

    private bool IsHost(Lobby lobby)
    {
        if (lobby.HostId == AuthenticationService.Instance.PlayerId)
        {
            return true;
        }
        return false;
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
                        { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerCreds.GetPlayerName())}
                    }
                },
                Data = new Dictionary<string, DataObject> {
                    {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, lobbyCred.gameMode) },
                    {"RelayCode", new DataObject(DataObject.VisibilityOptions.Member,"0")}
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

    public async void UpdateLobby(string relayCode)
    {
        try
        {
            UpdateLobbyOptions options = new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
                {
                    {"RelayCode", new DataObject(DataObject.VisibilityOptions.Member, relayCode) }
                }
            };

            Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(joinedLobby.Id,options);
            joinedLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void StartGame()
    {
        if (IsHost(joinedLobby))
        {
            SceneManager.LoadScene(3);
            string relayCode = await RelayControl.Instance.CreateRelay();
            UpdateLobby(relayCode);
        }
    }

}
