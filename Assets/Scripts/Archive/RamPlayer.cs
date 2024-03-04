using System.Collections;
using UnityEngine;

public class RamPlayer : EnemyMovementBehaviour
{
    public float acceleration;
    public float collisionDamage;
    public float sprintTime;
    private WaitForSeconds wait;

    private void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        brain = GetComponent<EnemyBrain>();
        wait = new WaitForSeconds(sprintTime);
    }
    public override void Evade()
    {
    }

    public override void Move()
    {
        if (brain.state == EnemyState.TooClose)
        {
            StartCoroutine(nameof(Sprint));
        }
        Vector2 nextpointDirection = brain.player.position - transform.position;
        enemyRigidbody2D.velocity = nextpointDirection.normalized * speed;
        AlignToMovement();
    }

    IEnumerator Sprint()
    {
        speed += acceleration;
        yield return wait;
        speed -= acceleration;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable player))
        {
            player.TakeDamage(collisionDamage);
        }
    }
}
