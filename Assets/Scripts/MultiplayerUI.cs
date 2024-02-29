using Unity.Netcode;

public class MultiPLayerUI : NetworkBehaviour
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
