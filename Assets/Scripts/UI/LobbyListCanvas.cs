using System;
using TMPro;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListCanvas : CanvasScript
{
    [SerializeField] private Canvas joinCanvas;
    [SerializeField] private Button refresh;
    [SerializeField] private Button edit;
    [SerializeField] private Button create;
    [SerializeField] private Button join;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject lobbyTextPrefab;

    private void Start()
    {
        refresh.onClick.AddListener(ListLobbies);
        create.onClick.AddListener(ShowNextCanvas);
        edit.onClick.AddListener(ShowPreviousCanvas);
        join.onClick.AddListener(ShowJoinCanvas);
    }

    private void ShowJoinCanvas()
    {
        joinCanvas.enabled = true;
        GetComponent<Canvas>().enabled = false;
    }

    public async void ListLobbies()
    {
        try
        {
            QueryResponse response = await Lobbies.Instance.QueryLobbiesAsync();
            foreach (Lobby lobby in response.Results)
            {
                CreateLobbyText(lobby);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public void CreateLobbyText(Lobby lobby)
    {
        TextMeshProUGUI[] texts = content.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in texts)
        {
            Destroy(text.gameObject);
        }
        GameObject t = Instantiate(lobbyTextPrefab, content);
        TextMeshProUGUI tText = t.GetComponentInChildren<TextMeshProUGUI>();
        tText.text = lobby.Name + " : " + lobby.AvailableSlots + " SLOTS AVAILABLE";
    }
}
