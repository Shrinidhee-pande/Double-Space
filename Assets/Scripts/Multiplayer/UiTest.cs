using Unity.Netcode;

public class UiTest : NetworkBehaviour
{
    public NetworkManager man;

    public void StartGameHost()
    {
        man.StartHost();
    }
    public void StartGameClient()
    {
        man.StartClient();
    }
    public void StartGameServer()
    {
        man.StartServer();
    }
}
