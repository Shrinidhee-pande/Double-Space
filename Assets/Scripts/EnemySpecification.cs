using UnityEngine;


public class EnemySpecification : MonoBehaviour, IDamageable
{
    public float healthPoints;
    public float collisionDamage;
    
    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
    }

    private void Update()
    {
        if (healthPoints <= 0)
        {
            Destroy(gameObject); 
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable player))
        {
            player.TakeDamage(collisionDamage);
        }
    }

    private void OnDestroy()
    {
        SpawnManagement.enemiesToSpawn--;
    }
}
