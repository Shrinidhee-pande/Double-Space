using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Class to handle PlayerInput and Movement
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D spaceshipRigidbody;
    private Projekt inputs;
    private InputAction movement;
    //private InputAction aim;
    private Weapon weapon;


    void Awake()
    {
        spaceshipRigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        inputs = new Projekt();
    }
    private void OnEnable()
    {
        movement = inputs.Player.Move;
        //aim = inputs.Player.Look;
        inputs.Player.Fire.performed += context =>
        {
            if (context.interaction is HoldInteraction)
            {
                weapon.rapidFire = true;
            }
            else
            {
                weapon.rapidFire = false;
            }
            StartFire();
        };
        inputs.Player.Fire.canceled += context => { StopFire(); };
        movement.performed += Move;
        movement.canceled += Move;
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Update()
    {
        Vector2 lookDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        weapon.gameObject.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 moveDirection = context.ReadValue<Vector2>();
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        spaceshipRigidbody.velocity = moveDirection * speed;
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
    }

    private void StopFire()
    {
        weapon.rapidFire = false;
    }

    private void StartFire()
    {
        weapon.Damage();
    }

    
}
