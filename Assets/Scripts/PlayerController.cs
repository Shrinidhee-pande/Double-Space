using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Class to handle Player Input and Movement
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float dodgeDistance;

    private Rigidbody2D spaceshipRigidbody;
    private PlayerControls inputs;
    private Weapon weapon;


    void Awake()
    {
        spaceshipRigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        inputs = new PlayerControls();
    }

    private void OnEnable()
    {
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
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