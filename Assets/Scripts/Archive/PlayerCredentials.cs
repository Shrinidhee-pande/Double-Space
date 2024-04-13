using UnityEngine;

public class PlayerCredentials : MonoBehaviour
{
    private string playerName;
    private string playerId;

    public void SetPlayerId(string id)
    {
        playerId = id;
    }

    public void SetPlayerName(string namePlayer)
    { 
        playerName = namePlayer; 
    }

    public string GetPlayerName()
    {
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "player" + Random.Range(1, 101);
            return playerName;
        }
        return playerName; 
    }

    public string GetPlayerId()
    { 
        return playerId; 
    }

}
