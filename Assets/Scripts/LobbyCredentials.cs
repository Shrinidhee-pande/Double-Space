using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCredentials : MonoBehaviour
{
    public string lobbyName;
    public bool IsPrivate;

    public void SetLobbyName(string name)
    {
        Debug.Log($"Called {name}");
        lobbyName = name;
    }
    public void SetLobbyPrivacy(bool priv)
    {
        Debug.Log("Called");
        IsPrivate = priv;
    }
}
