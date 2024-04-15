using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobbyCanvas : CanvasScript
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Button mode;
    [SerializeField] private Button create;
    [SerializeField] private Button cancel;

    private Mode gameMode;

    void Start()
    {
        input.onEndEdit.AddListener(LobbyData.Instance.SetLobbyName);
        toggle.onValueChanged.AddListener(LobbyData.Instance.SetLobbyPrivacy);
        mode.onClick.AddListener(ChangeGameMode);
        create.onClick.AddListener(ShowNextCanvas);
        cancel.onClick.AddListener(ShowPreviousCanvas);
    }
    private void Update()
    {
        if (string.IsNullOrEmpty(input.text))
        {
            create.interactable = false;
        }
        else
        {
            create.interactable = true;
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

    public override void ShowNextCanvas()
    {
        CreateLobbyByName();
        base.ShowNextCanvas();
    }

    public async void CreateLobbyByName()
    {
        try
        {
            CreateLobbyOptions options = new CreateLobbyOptions
            {
                IsPrivate = LobbyData.Instance.GetLobbyPrivacy(),
                Player = new Player
                {
                    Data = new Dictionary<string, PlayerDataObject> {
                        { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, PlayerData.Instance.GetPlayerName())}
                    }
                },
                Data = new Dictionary<string, DataObject> {
                    {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, LobbyData.Instance.GetGameMode()) },
                    {"RelayCode", new DataObject(DataObject.VisibilityOptions.Member,"0")}
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(LobbyData.Instance.GetLobbyName(), 2, options);
            LobbyData.Instance.SetLobby(lobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
