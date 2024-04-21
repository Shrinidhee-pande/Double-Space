using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private int damage; 
    [SerializeField] private float speed;
    public Vector2 velocity;
    public float gunRange;

    private Rigidbody2D bulletRigidbody;
    private float timeToLive;

    public override void OnNetworkSpawn()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();

        bulletRigidbody.velocity = velocity * speed;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);

        timeToLive = gunRange / speed;
        StartCoroutine(Destroy(timeToLive));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageAble))
        {
            damageAble.TakeDamage(damage);
        }
        StartCoroutine(Destroy(0f));
    }


    IEnumerator Destroy(float timeToLive)
    {
        yield return new WaitForSeconds(timeToLive);
        if(IsHost)
        GetComponent<NetworkObject>().Despawn(true);
    }
}
