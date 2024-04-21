using Unity.Netcode;
using UnityEngine;

public class EnemyBrain : NetworkBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;

    public Transform nearestPlayer
    {
        get; private set;
    }


    private int health;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        nearestPlayer = FindObjectOfType<PlayerCore>().transform;
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }

}
