using System;
using UnityEngine;

public class PlayerSpecification : MonoBehaviour, IDamageable
{
    public class OnChangeEventArgs : EventArgs
    {
        public float HealthPoints { get; set; }
        public float ExpPoints { get; set; }
        public OnChangeEventArgs(float hp, float expPoints)
        {
            HealthPoints = hp;
            ExpPoints = expPoints;
        }
    }
    public delegate void PlayerSpecificationDelegate(OnChangeEventArgs e);
    public static event PlayerSpecificationDelegate OnSpecChange;

    [SerializeField] private float healthPoints;
    [SerializeField] private int level;
    [SerializeField] private float collisionDamage;
    private float expPoints;


    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        OnSpecChange?.Invoke(new OnChangeEventArgs(healthPoints,expPoints));
    }

    private void Update()
    {
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.currentState = GameState.GameOver;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<IDamageable>(out IDamageable other))
        {
            other.TakeDamage(collisionDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            Debug.Log("Win");
        }
    }
    private void OnDestroy()
    {
       GameManager.Instance.currentState = GameState.GameOver;
    }
}
