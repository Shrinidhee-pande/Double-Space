using Unity.Netcode;
using UnityEngine;

public class PlayerControl : NetworkBehaviour
{
    public float speed;
    public float dodgeDistance;
    public Camera thisCamera;
    private Rigidbody2D spaceshipRigidbody;
    private Weapon weapon;
    private Vector2 moveDirection;


    private void Awake()
    {
        spaceshipRigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
    }
    private void Update()
    {
        if (!IsOwner)
            return;

        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        spaceshipRigidbody.velocity = moveDirection * speed;

        Vector3 mousePosition = thisCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector2 dir = spaceshipRigidbody.velocity.normalized;
            spaceshipRigidbody.MovePosition((Vector2)transform.position + dir * dodgeDistance);
        }

        if (Input.GetMouseButton(0))
        {
            weapon.HoldFire = true;
            weapon.Damage();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.HoldFire = false;
        }
    }
}
