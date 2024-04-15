using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyData : MonoBehaviour
{
    public static LobbyData Instance;

    private Lobby currentLobby;
    private string lobbyName;
    private string lobbyCode;
    private string gameMode;
    private bool IsPrivate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLobbyName(string name)
    {
        lobbyName = name;
    }
    public void SetLobbyCode(string code)
    {
        lobbyCode = code;
    }
    public void SetGameMode(string mode)
    {
        gameMode = mode;
    }
    public void SetLobbyPrivacy(bool priv)
    {
        IsPrivate = priv;
    }
    public void SetLobby(Lobby lobby)
    {
        currentLobby = lobby;
    }
    public string GetLobbyName()
    {
        return lobbyName;
    }
    public string GetLobbyCode()
    {
        return lobbyCode;
    }
    public string GetGameMode()
    {
        return gameMode;
    }
    public bool GetLobbyPrivacy()
    {
        return IsPrivate;
    }
    public Lobby GetLobby()
    {
        return currentLobby;
    }




}
