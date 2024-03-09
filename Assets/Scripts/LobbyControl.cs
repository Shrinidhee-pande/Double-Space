using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyControl : MonoBehaviour
{
    public RectTransform createCanvas;
    public RectTransform joinCanvas;


    private Lobby currentLobby;
    

    private async void Start()
    {
        createCanvas.gameObject.SetActive(false);
        joinCanvas.gameObject.SetActive(false);
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In As: " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        ListLobbies();
    }

    public void CreateLobbyButton()
    {
        createCanvas.gameObject.SetActive(true); 
    }
    public void JoinLobbyButton()
    {
        joinCanvas.gameObject.SetActive(true);
    }

    public async void CreateLobbyByName()
    {
        LobbyCredentials lobbyCred = GetComponent<LobbyCredentials>();
        string name = lobbyCred.lobbyName;
        bool privateLobby = lobbyCred.IsPrivate;
        try
        {

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(name, 2, new CreateLobbyOptions { IsPrivate = privateLobby });
            Debug.Log("Lobby created: " + lobby.Id + " " + lobby.Name);
            currentLobby = lobby;
            createCanvas.gameObject.SetActive(false);
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
            foreach(Lobby lobby in response.Results)
            {
                Debug.Log(lobby.Name);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
