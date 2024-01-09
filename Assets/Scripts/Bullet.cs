using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public float timeToLive;

    public Bullet()
    {
        direction = Vector2.up;
        timeToLive = 3f;
    }

    public Bullet(Vector2 velocity,float time)
    {
        direction = velocity;
        timeToLive = time;
    }
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = direction;
        Destroy(gameObject, timeToLive);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
