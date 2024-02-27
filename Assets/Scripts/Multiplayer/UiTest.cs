using Unity.Netcode;

public class UiTest : NetworkBehaviour
{
    public void StartGameHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void StartGameClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void StartGameServer()
    {
        NetworkManager.Singleton.StartServer();
    }
}
