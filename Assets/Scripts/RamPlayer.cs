using UnityEngine;

public class RamPlayer : EnemyMovementBehaviour
{
    public float acceleration;
    private void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
    }
    public override void Evade()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        if(EnemyBrain.state == EnemyState.TooClose)
        {
            speed += acceleration;
        }
        Vector2 nextpointDirection = EnemyBrain.player.position - transform.position;
        enemyRigidbody2D.velocity = nextpointDirection.normalized * speed;
        AlignToMovement();
    }
}
