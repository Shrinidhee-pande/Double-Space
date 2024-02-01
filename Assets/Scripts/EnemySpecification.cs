using UnityEngine;

public class EnemySpecification : MonoBehaviour, IDamageable
{
    public float healthPoints;
    
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

}
