using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
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
        TextMeshProUGUI[] texts = content.GetComponentsInChildren<TextMeshProUGUI>();
        if (texts.Length > 0)
        {
            foreach(TextMeshProUGUI text in texts)
            {
                Destroy(text.gameObject);
            }
        }
        GameObject t = Instantiate(lobbyTextPrefab, content);
        TextMeshProUGUI tText = t.GetComponentInChildren<TextMeshProUGUI>();
        tText.text = lobby.Name + " : " + lobby.AvailableSlots + " SLOTS AVAILABLE";
        Debug.Log(lobby.Name);
    }


    public void CreatePlayerText(List<LobbyPlayerJoined> players)
    {
        TextMeshProUGUI[] texts = playerContent.GetComponentsInChildren<TextMeshProUGUI>();
        if (texts.Length > 0)
        {
            foreach (TextMeshProUGUI text in texts)
            {
                Destroy(text.gameObject);
            }
        }
        foreach (LobbyPlayerJoined p in players)
        {
            GameObject t = Instantiate(lobbyTextPrefab, playerContent);
            TextMeshProUGUI tText = t.GetComponentInChildren<TextMeshProUGUI>();
            tText.text = p.Player.Data["PlayerName"].Value;
        }
    }

    public void CreatePlayerText(Lobby lobby)
    {
        TextMeshProUGUI[] texts = playerContent.GetComponentsInChildren<TextMeshProUGUI>();
        if (texts.Length > 0)
        {
            foreach (TextMeshProUGUI text in texts)
            {
                Destroy(text.gameObject);
            }
        }
        foreach (Player p in lobby.Players)
        {
            GameObject t = Instantiate(lobbyTextPrefab, playerContent);
            TextMeshProUGUI tText = t.GetComponentInChildren<TextMeshProUGUI>();
            tText.text = p.Data["PlayerName"].Value;
        }
    }

    public void UpdateLobbyText(Lobby lobby)
    {
        lobbyDetails.text = lobby.Name + "\tCode: " + lobby.LobbyCode + "\t" +  lobby.Data["GameMode"].Value;
    }

}
