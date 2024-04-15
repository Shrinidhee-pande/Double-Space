using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;


public enum GameState
{
    Play, GameOver
};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    [SerializeField] private Canvas startCanvas;

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

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        PlayerData.Instance.SetPlayerId(AuthenticationService.Instance.PlayerId);
        Debug.Log("Signed In As: " + AuthenticationService.Instance.PlayerId);

        currentState = GameState.Play;
        startCanvas.enabled = true;
    }
}
