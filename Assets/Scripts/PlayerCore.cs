using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerCore : MonoBehaviour, IDamageable
{
    public class OnChangeEventArgs : EventArgs
    {
        public float HealthPoints { get; set; }
        public OnChangeEventArgs(float hp)
        {
            HealthPoints = hp;
        }
    }
    public delegate void PlayerUpdateDelegate(OnChangeEventArgs e);
    public static event PlayerUpdateDelegate OnSpecChange;

    [SerializeField] private int maxHealth;
    [SerializeField] private int collisionDamage; 

    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnSpecChange?.Invoke(new OnChangeEventArgs(maxHealth));

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<IDamageable>(out IDamageable other))
        {
            other.TakeDamage(collisionDamage);
        }
    }
}
