using UnityEngine;

[RequireComponent(typeof(EnemyBrain))]
public abstract class EnemyMovementBehaviour : MonoBehaviour
{
    public float speed;

    protected Rigidbody2D enemyRigidbody2D;

    public abstract void Move();
    public abstract void Evade();

    /// <summary>
    /// Align to velocity by default
    /// </summary>
    protected virtual void AlignToMovement()
    {
        float angle;
        Vector2 velocity = enemyRigidbody2D.velocity;
        angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

}
