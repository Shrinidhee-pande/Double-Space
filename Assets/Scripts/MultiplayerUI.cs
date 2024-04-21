using Unity.Netcode;
using UnityEngine;

public class MultiPLayerUI : MonoBehaviour
{

    public void StartGameHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void StartGameServer()
    {
        NetworkManager.Singleton.StartServer();
    }
        
    public void StartGameClient()
    {
        NetworkManager.Singleton.StartClient();
    }


    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
