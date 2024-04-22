using Unity.Netcode;
using UnityEngine;

public enum EnemyState
{
    Pursue, Attack
}
public class EnemyBrain : NetworkBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;

    public Transform NearestPlayer
    {
        get; private set;
    }
    public static EnemyState State
    {
        get; set;
    }

    private int health;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        health = maxHealth;
        State = EnemyState.Pursue;
    }

    private void Update()
    {
        if(State  == EnemyState.Pursue)
        {
            NearestPlayer = FindNearestPlayer(FindObjectsOfType<PlayerCore>());
        }
    }

    private Transform FindNearestPlayer(PlayerCore[] list)
    {
        float minDistance = Vector2.Distance(transform.position, list[0].transform.position);
        foreach (PlayerCore item in list)
        {
            if (minDistance > Vector2.Distance(transform.position, item.transform.position))
            {
                return item.transform;
            }
        }
        return list[0].transform;
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
        SpawnManager.enemiesKilled += 1;
        GetComponent<NetworkObject>().Despawn(true);
    }

}
