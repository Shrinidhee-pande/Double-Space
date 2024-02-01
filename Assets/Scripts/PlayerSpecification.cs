using System;
using UnityEngine;

public class PlayerSpecification : MonoBehaviour, IDamageable
{
    public class OnChangeEventArgs : EventArgs
    {
        public float HealthPoints { get; set; }
        public OnChangeEventArgs(float hp)
        {
            HealthPoints = hp;
        }
    }
    public delegate void PlayerSpecificationDelegate(OnChangeEventArgs e);
    public static event PlayerSpecificationDelegate OnSpecChange;

    [SerializeField] private float healthPoints;
    [SerializeField] private int level;


    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        OnSpecChange?.Invoke(new OnChangeEventArgs(healthPoints));
    }

    private void Update()
    {
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
            GameManager.CurrentState = GameState.GameOver;
        }
    }
}
