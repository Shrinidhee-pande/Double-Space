using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public bool useMovement;
    public bool useDamage;
    public Transform player;
    public float speed;

    protected Rigidbody2D enemyRigidbody;

    public abstract void MovementBehavior();
    public abstract void DamageBehaviour();
    public abstract void AlignToMovement();
}
