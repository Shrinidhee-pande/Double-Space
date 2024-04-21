using Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Class to handle Player Input and Movement
/// </summary>
public class InputHandler : NetworkBehaviour
{

    [SerializeField] private CinemachineVirtualCamera thisCamera;
    [SerializeField] private AudioListener listener;

    private MainInput inputs;
    private PlayerMovement movement;
    private Weapon weapon;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
        inputs = new MainInput();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            thisCamera.Priority = 1;
            listener.enabled = true;
        }
        else
        {
            thisCamera.Priority = 0;
        }
    }

    private void OnEnable()
    {
        //Move
        inputs.Player.Move.performed += (context) => 
        { 
            movement.Move(context.ReadValue<Vector2>()); 
        };
        inputs.Player.Move.canceled += (context) => { movement.Move(context.ReadValue<Vector2>()); };

        //Aim
        inputs.Player.Aim.performed += (context) => { movement.Aim(context.ReadValue<Vector2>()); };

        //Fire
        inputs.Player.Fire.performed += (context) =>
        {
            if (context.interaction is HoldInteraction)
            {
                weapon.HoldFire = true;
            }
            else
            {
                weapon.HoldFire = false;
            }
            weapon.Fire();
        };
        inputs.Player.Fire.canceled += (context) =>
        {
            weapon.HoldFire = false;
        };

        //Dodge
        inputs.Player.Dodge.performed += (context) =>
        {
            StartCoroutine(movement.Dodge());
        };

        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}