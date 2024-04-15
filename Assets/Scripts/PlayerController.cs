using Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Class to handle Player Input and Movement
/// </summary>
public class PlayerController : NetworkBehaviour
{
    public float speed;
    public float dodgeDistance;
    public CinemachineVirtualCamera thisCamera;
    public AudioListener listener;

    private Rigidbody2D spaceshipRigidbody;
    private MainInput inputs;
    private Weapon weapon;
    private Camera cam;

    private void Awake()
    {
        spaceshipRigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        inputs = new MainInput();
        cam = GetComponentInChildren<Camera>();
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
        if(!IsLocalPlayer) { return; }
        //Move
        inputs.Player.Move.performed += Move;
        inputs.Player.Move.canceled += Move;

        //Aim
        inputs.Player.Aim.performed += Aim;

        //Fire
        inputs.Player.Fire.performed += context =>
        {
            if (context.interaction is HoldInteraction)
            {
                weapon.HoldFire = true;
            }
            else
            {
                weapon.HoldFire = false;
            }
            StartFire();
        };
        inputs.Player.Fire.canceled += context =>
        {
            StopFire();
        };

        //Dodge
        inputs.Player.Dodge.performed += context =>
        {
            Dodge();
        };

        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 moveDirection = context.ReadValue<Vector2>();
        spaceshipRigidbody.velocity = moveDirection * speed;
    }

    private void Aim(InputAction.CallbackContext context)
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
        Vector2 lookDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Dodge()
    {
        Vector2 dir = spaceshipRigidbody.velocity.normalized;
        spaceshipRigidbody.MovePosition((Vector2)transform.position + dir * dodgeDistance);
    }

    private void StartFire()
    {
        weapon.Damage();
    }

    private void StopFire()
    {
        weapon.HoldFire = false;
    }
}