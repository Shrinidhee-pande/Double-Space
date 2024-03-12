using UnityEngine;

public class LobbyCredentials : MonoBehaviour
{
    public string lobbyName;
    public string lobbyCode;
    public bool IsPrivate;

    public void SetLobbyName(string name)
    {
        lobbyName = name;
    }
    public void SetLobbyCode(string code)
    {
        lobbyCode = code;
    }
    public void SetLobbyPrivacy(bool priv)
    {
        Debug.Log(priv);
        IsPrivate = priv;
    }




}
