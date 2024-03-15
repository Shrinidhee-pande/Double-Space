using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public float damage;
    public Vector2 velocity;
    public float timeToLive;
    private Rigidbody2D bulletRigidbody;

    public override void OnNetworkSpawn()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = velocity;
        Destroy(gameObject, timeToLive);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageAble))
        {
            damageAble.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
