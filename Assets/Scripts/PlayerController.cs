using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
        weapon = GetComponent<Weapon>();
        inputs = new Projekt();
    }
    private void OnEnable()
    {
        movement = inputs.Player.Move;
        //aim = inputs.Player.Look;
        inputs.Player.Fire.performed += _ => StartFire();
        inputs.Enable();
    }
    /*
    private void StopFire()
    {
        weapon.rapidFire = false;
    }*/
    private void StartFire()
    {
        weapon.Damage();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
    
    private void Update()
    {
        Vector2 lookDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x)*Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);
    }
    private void FixedUpdate()
    {
        Vector2 moveDirection = movement.ReadValue<Vector2>();
        spaceshipRigidbody.velocity = moveDirection * speed;
    }

}
