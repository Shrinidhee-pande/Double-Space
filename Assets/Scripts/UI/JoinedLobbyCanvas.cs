using System;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoinedLobbyCanvas : CanvasScript
{
    [SerializeField] private TextMeshProUGUI lobbyText;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Button mode;
    [SerializeField] private Button ready;

    private Mode gameMode;
    private float heartbeatCounter;
    private float lobbyUpdateCounter;

    private bool IsHost()
    {
        return LobbyData.Instance.GetLobby().HostId == AuthenticationService.Instance.PlayerId;
    }

    void Start()
    {
        ready.onClick.AddListener(StartGame);
        mode.onClick.AddListener(UpdateLobbyGameMode);
    }

    private void Update()
    {
        if (LobbyData.Instance.GetLobby() != null)
        {
            Lobby lobby = LobbyData.Instance.GetLobby();
            lobbyText.text = $"Lobby\n {lobby.Name} \t CODE: {lobby.LobbyCode}";
            HeartbeatHandler();
            LobbyUpdateHandler();
        }
    }

    private async void StartGame()
    {
        if (!IsHost()) { ready.interactable = false; }
        GameMode.Instance.mode = gameMode;
        string relayCode = await RelayControl.Instance.CreateRelay();
        UpdateLobbyOptions options = new UpdateLobbyOptions
        {
            Data = new Dictionary<string, DataObject> {
                    {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, LobbyData.Instance.GetGameMode()) },
                    {"RelayCode", new DataObject(DataObject.VisibilityOptions.Member,relayCode)}
            }
        };
        try
        {
            await LobbyService.Instance.UpdateLobbyAsync(LobbyData.Instance.GetLobby().Id, options);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void UpdateLobbyGameMode()
    {
        if (!IsHost())
        {
            mode.interactable = false;
            mode.GetComponentInChildren<TextMeshProUGUI>().text = LobbyData.Instance.GetGameMode();
        }

        ChangeGameMode();
        UpdateLobbyOptions options = new UpdateLobbyOptions
        {
            Data = new Dictionary<string, DataObject> {
                    {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, LobbyData.Instance.GetGameMode()) },
                    {"RelayCode", new DataObject(DataObject.VisibilityOptions.Member,"0")}
            }
        };
        try
        {
            await LobbyService.Instance.UpdateLobbyAsync(LobbyData.Instance.GetLobby().Id, options);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private void ChangeGameMode()
    {
        switch (gameMode)
        {
            default:
            case Mode.Sabotage:
                gameMode = Mode.Survival;
                break;
            case Mode.Survival:
                gameMode = Mode.Sabotage;
                break;
        }
        mode.GetComponentInChildren<TextMeshProUGUI>().text = gameMode.ToString();
        LobbyData.Instance.SetGameMode(gameMode.ToString());
    }

    private async void LobbyUpdateHandler()
    {
        if (LobbyData.Instance.GetLobby() != null)
        {
            lobbyUpdateCounter -= Time.deltaTime;

            if (lobbyUpdateCounter < 0f)
            {
                lobbyUpdateCounter += 2f;
                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(LobbyData.Instance.GetLobby().Id);
                LobbyData.Instance.SetLobby(lobby);
                CreatePlayerData();

                if (lobby.Data["RelayCode"].Value != "0")
                {
                    if (!IsHost())
                    {
                        SceneManager.LoadScene(1);
                        RelayControl.Instance.JoinRelay(lobby.Data["RelayCode"].Value);
                    }
                }

            }
        }
    }

    private async void HeartbeatHandler()
    {
        if (IsHost())
        {
            heartbeatCounter -= Time.deltaTime;

            if (heartbeatCounter < 0f)
            {
                heartbeatCounter += 25f;
                await LobbyService.Instance.SendHeartbeatPingAsync(LobbyData.Instance.GetLobby().Id);
            }
        }
    }

    private void CreatePlayerData()
    {
        TextMeshProUGUI[] texts = content.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in texts)
        {
            Destroy(text.gameObject);
        }
        Lobby lobby = LobbyData.Instance.GetLobby();
        for (int i = 0; i<lobby.Players.Count; i++)
        {
            GameObject t = Instantiate(prefab, content);
            TextMeshProUGUI tText = t.GetComponentInChildren<TextMeshProUGUI>();
            tText.text = lobby.Players[i].Data["PlayerName"].Value;
        }
    }
}
