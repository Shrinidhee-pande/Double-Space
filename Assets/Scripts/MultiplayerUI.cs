using Unity.Netcode;
using UnityEngine;

public class MultiPLayerUI : NetworkBehaviour
{
    public Canvas[] canvases;

    private void Start()
    {
        canvases[1].gameObject.SetActive(false);
    }
    public void StartGameHost()
    {
        NetworkManager.Singleton.StartHost();
        canvases[0].gameObject.SetActive(false);
        canvases[1].gameObject.SetActive(true);
    }
    public void StartGameClient()
    {
        NetworkManager.Singleton.StartClient();
        canvases[0].gameObject.SetActive(false);
        canvases[1].gameObject.SetActive(true);
    }
}
