using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Class to handle PlayerInput and Movement
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float dodgeDistance;

    private Rigidbody2D spaceshipRigidbody;
    private Projekt inputs;
    private Weapon weapon;


    void Awake()
    {
        spaceshipRigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        inputs = new Projekt();
    }
    private void OnEnable()
    {
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
        inputs.Player.Move.performed += Move;
        inputs.Player.Move.canceled += Move;
        inputs.Player.Dodge.performed += context =>
        {
            Dodge();
        };
        inputs.Enable();
    }

    private void Dodge()
    {
        Vector2 dir = spaceshipRigidbody.velocity.normalized;
        spaceshipRigidbody.MovePosition((Vector2)transform.position + dir * dodgeDistance);
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 lookDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        /*float thisAngle = transform.eulerAngles.z;
        if(angle >= thisAngle + 60)
        {
            angle = thisAngle + 60;
        }
        else if (angle <= thisAngle - 60)
        {
            angle = thisAngle - 60;
        }*/
        weapon.gameObject.transform.eulerAngles = new Vector3(0, 0, angle);

    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 moveDirection = context.ReadValue<Vector2>();
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        spaceshipRigidbody.velocity = moveDirection * speed;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void StopFire()
    {
        weapon.HoldFire = false;
    }

    private void StartFire()
    {
        weapon.Damage();
    }
}