using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class FollowBehaviour : EnemyBehaviour
{
    public float minDistance;
    public float maxDistance;
    public float nextTime;

    private EnemyState currently;
    private Weapon weapon;
    private float nextFireTime;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
        //currently = .OutOfRange;
    }

    public override void AlignToMovement()
    {/*
        float angle;
        if (currently == State.OutOfRange)
        {
            Vector2 velocity = enemyRigidbody.velocity;
            angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        }
        else
        {
            Vector2 dir = player.position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
        transform.eulerAngles = new Vector3(0, 0, angle);
    */}

    public override void DamageBehaviour()
    {/*
        if (currently != State.OutOfRange && nextFireTime < Time.time)
        {
            weapon.Damage();
            nextFireTime = Time.time + nextTime;
        }*/
    }

    public override void MovementBehavior()
    {/*
        float distance = Vector2.Distance(player.position, transform.position);
        Vector2 direction = player.position - transform.position;

        if (distance > maxDistance)
        {
            currently = State.OutOfRange;
            enemyRigidbody.velocity = direction.normalized * speed;
        }
        else if (distance > minDistance)
        {
            currently = State.InRange;
            enemyRigidbody.velocity = transform.up * speed;
        }
        else
        {
            currently = State.TooClose;
            enemyRigidbody.velocity = -1 * speed * direction.normalized;
        }
        AlignToMovement();*/
    }
}
