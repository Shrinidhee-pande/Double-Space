using UnityEngine;

public enum EnemyState
{
    Follow, Wander,
};
public class EnemyBehaviour : MonoBehaviour
{
    public EnemyState State { get; set; }
    public Vector2 Bounds;
    public Vector2 posBound;
    public float detectionRadius;
    public LayerMask layerMask;
    public float followSpeed;
    public LayerMask excludeMask;


    private Transform player;
    private Rigidbody2D enemyRigidbody;
    private Vector2 nextPoint;
    private SpriteRenderer sprite;

    void Start()
    {
        State = EnemyState.Wander;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        nextPoint = GetNextPoint();
        enemyRigidbody.excludeLayers = excludeMask;
    }

    private Vector2 GetNextPoint()
    {
        float x, y;
        if (transform.position.x < -posBound.x || transform.position.y < -posBound.y)
        {
            x = Random.Range(0, Bounds.x);
            y = Random.Range(0, Bounds.y);
        }
        else if (transform.position.x > posBound.x || transform.position.y > posBound.y)
        {
            x = Random.Range(-Bounds.x, Bounds.x);
            y = Random.Range(-Bounds.y, Bounds.y);
        }
        else
        {
            x = Random.Range(-Bounds.x, Bounds.x);
            y = Random.Range(-Bounds.y, Bounds.y);
        }
        return new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable player))
        {
            player.TakeDamage(50);
            GameManager.CurrentState = GameState.GameOver;
        }
    }
    void FixedUpdate()
    {
        if (State == EnemyState.Wander)
        {
            if (Vector2.Distance(transform.position, nextPoint) <= 5f)
            {
                nextPoint = GetNextPoint();
            }
            Vector2 nextpointDirection = nextPoint - (Vector2)transform.position;
            enemyRigidbody.velocity = nextpointDirection.normalized;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);
            if (hits != null)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.name == "Player")
                    {
                        State = EnemyState.Follow;
                        player = hit.transform;
                    }
                }
            }
        }
        if(State == EnemyState.Follow)
        {
            sprite.color = Color.red;
            if (player != null)
            {
                Vector2 nextpointDirection = player.position - transform.position;
                enemyRigidbody.velocity = nextpointDirection.normalized * followSpeed;
            }
        }
    }
}
