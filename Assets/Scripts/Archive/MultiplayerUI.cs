using Unity.Netcode;
using UnityEngine;

public class MultiPLayerUI : MonoBehaviour
{

    public void StartGameHost()
    {
        NetworkManager.Singleton.StartHost();
    }
        
    public void StartGameClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
