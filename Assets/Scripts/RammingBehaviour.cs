using UnityEngine;

public class RammingBehaviour : EnemyBehaviour
{
    public float sprintDistance;

    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }
    public override void AlignToMovement()
    {
        Vector2 velocity = enemyRigidbody.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public override void MovementBehavior()
    {
        Vector2 nextpointDirection = player.position - transform.position;
        enemyRigidbody.velocity = nextpointDirection.normalized * speed;
        AlignToMovement();
    }

    public override void DamageBehaviour()
    {
        Vector2 dir = enemyRigidbody.velocity.normalized;
        enemyRigidbody.MovePosition((Vector2)transform.position + dir * sprintDistance);
    }
}
