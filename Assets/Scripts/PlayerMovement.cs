using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dodgeCooldown;
    [SerializeField] private float dodgeSpeed;

    private bool canDash;
    private Rigidbody2D playerRigidbody;
    private Camera cam;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();
        canDash = true;
    }

    public void Move(Vector2 moveDirection)
    {
        playerRigidbody.velocity = moveDirection * speed;
    }

    public void Aim(Vector3 mousePosition)
    {
        Vector3 position = cam.ScreenToWorldPoint(mousePosition);
        Vector2 lookDirection = (position - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public IEnumerator Dodge()
    {
        if (!canDash)
        {
            yield break;
        }

        canDash = false;
        playerRigidbody.velocity *= dodgeSpeed;
        yield return new WaitForSeconds(0.1f);
        playerRigidbody.velocity /= dodgeSpeed;

        yield return new WaitForSeconds(dodgeCooldown);
        canDash = true;
    }
}
