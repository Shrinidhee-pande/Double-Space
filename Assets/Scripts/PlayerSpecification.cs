using UnityEngine;

public class PlayerSpecification : MonoBehaviour, IDamageable
{
    [SerializeField] private float healthPoints;
    [SerializeField] private int level;
    public void TakeDamage(float damage)
    {

    }
}
