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
        if (LobbyData.Instance.GetLobby() != null)
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

    
    /*public async void ListLobbies()
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
    }*/

    

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
            SceneManager.LoadScene(1);
            string relayCode = await RelayControl.Instance.CreateRelay();
            UpdateLobby(relayCode);
        }
    }

}
