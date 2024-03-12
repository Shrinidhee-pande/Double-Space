using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyUi : MonoBehaviour
{
    public static LobbyUi Instance;
    public RectTransform[] canvases;
    public TextMeshProUGUI lobbyDetails;
    public GameObject lobbyTextPrefab;
    public Transform content;
    public Transform playerContent;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        EnableCanvas(0);
    }

    public void EnableCanvas(int index)
    {
        for(int i = 0; i < canvases.Length; i++)
        {
            canvases[i].gameObject.SetActive(false);
        }
        canvases[index].gameObject.SetActive(true);
    }

    public void CreateLobbyText(Lobby lobby)
    {
        GameObject t = Instantiate(lobbyTextPrefab, content);
        TextMeshProUGUI tText = t.GetComponentInChildren<TextMeshProUGUI>();
        tText.text = lobby.Name + " : " + lobby.AvailableSlots + " : " + lobby.LobbyCode;
        Debug.Log(lobby.Name);
    }


    public void CreatePlayerText(Lobby lobby)
    {
        lobbyDetails.text = lobby.Name + "\tAvailable Slots: " + lobby.AvailableSlots + "\tCode: " + lobby.LobbyCode;
        List<Player> players = lobby.Players;
        foreach (Player p in players)
        {
            GameObject t = Instantiate(lobbyTextPrefab, playerContent);
            TextMeshProUGUI tText = t.GetComponentInChildren<TextMeshProUGUI>();
            tText.text = p.Data["PlayerName"].Value;
        }
    }
}
