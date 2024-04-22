using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;


public enum GameState
{
    Lobby, JoinGame, Play, GameOver
};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    private GameObject lobby;
    private GameObject game;

    public async void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        PlayerData.Instance.SetPlayerId(AuthenticationService.Instance.PlayerId);
        Debug.Log("Signed In As: " + AuthenticationService.Instance.PlayerId);

        currentState = GameState.Lobby;

        NetworkManager.Singleton.OnClientDisconnectCallback += ClientDisconnected;

    }

    private void Start()
    {
        lobby = FindObjectOfType<Lobbi>().gameObject;
        game = FindObjectOfType<Game>().gameObject;
    }
    private void ClientDisconnected(ulong obj)
    {
        currentState = GameState.GameOver;
    }

    private void Update()
    {
        if (lobby != null && game != null)
        {

            switch (currentState)
            {
                case GameState.Lobby:
                    lobby.SetActive(true);
                    game.SetActive(false);
                    break;
                case GameState.JoinGame:
                    lobby.SetActive(false);
                    game.SetActive(true);
                    Time.timeScale = 0;
                    break;
                case GameState.Play:
                    Time.timeScale = 1;
                    break;
                case GameState.GameOver:
                    FindObjectOfType<GameOverCanvas>(true).gameObject.SetActive(true);
                    Time.timeScale = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
