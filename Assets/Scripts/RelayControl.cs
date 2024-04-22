using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelayControl : MonoBehaviour
{
    public static RelayControl Instance;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public async Task<string> CreateRelay()
    {
        try
        {
            ChangeScene();
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            RelayServerData relayData = new RelayServerData(allocation, "wss");

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);
            NetworkManager.Singleton.StartHost();

            Debug.Log($"Relay Created with Join Code {joinCode}");
            return joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
            return null;
        }
    }

    private static void ChangeScene()
    {
        GameManager.Instance.currentState = GameState.JoinGame;
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            ChangeScene();
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayData = new RelayServerData(allocation, "wss");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);
            NetworkManager.Singleton.StartClient();

            Debug.Log($"Relay Joined with Join Code {joinCode}");
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
        }
    }
}
