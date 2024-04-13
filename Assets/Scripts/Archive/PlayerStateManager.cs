using UnityEngine;

public enum PlayerState
{
    Play,
    Animate
}
public class PlayerStateManager : MonoBehaviour
{
    public static PlayerState CurrentState { get; set; }
}
