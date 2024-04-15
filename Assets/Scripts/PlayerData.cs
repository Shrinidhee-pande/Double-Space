using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    private string playerName;
    private string playerId;
    private Sprite shipName;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetPlayerId(string id)
    {
        playerId = id;
    }

    public void SetPlayerName(string namePlayer)
    { 
        playerName = namePlayer; 
    }
    
    public void SetShip(Sprite name)
    {
        shipName = name;
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
    
    public Sprite GetShip()
    { 
        return shipName; 
    }

}
