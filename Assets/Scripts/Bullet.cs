using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public Vector2 velocity;
    public float timeToLive;
    public LayerMask excludeMask;
    private Rigidbody2D bulletRigidbody;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.excludeLayers = excludeMask;
        bulletRigidbody.velocity = velocity;
        Destroy(gameObject, timeToLive);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
