using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dodgeCooldown;
    [SerializeField] private float dodgeSpeed;

    private bool canDash;
    private Rigidbody2D thisRigidbody;
    private Transform currentPlayer;

    private void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentPlayer = GetComponent<EnemyBrain>().NearestPlayer;
        if (currentPlayer != null)
        {
            MoveToPlayer(currentPlayer.position);
        }
    }

    private void MoveToPlayer(Vector3 position)
    {
        Vector2 direction = (position - transform.position).normalized;
        thisRigidbody.velocity = direction * speed;
        LookAt(position);
    }

    private void LookAt(Vector3 position)
    {
        Vector2 lookDirection = (position - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
