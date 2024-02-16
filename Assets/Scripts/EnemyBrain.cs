using UnityEngine;

public class EnemyBrain : MonoBehaviour 
{
    EnemyBehaviour[] behaviours;

    public delegate void EnemyMovementDelegate();
    public EnemyMovementDelegate Move;

    public delegate void EnemyDamageDelegate();
    public EnemyDamageDelegate Damage;

    void Start()
    {
        behaviours = GetComponents<EnemyBehaviour>();
        for (int i = 0; i < behaviours.Length; i++)
        {
            behaviours[i].player = FindObjectOfType<PlayerSpecification>().transform;
            if (behaviours[i].useMovement)
            {
                Move = behaviours[i].MovementBehavior;
            }
            if (behaviours[i].useDamage)
            {
                Damage = behaviours[i].DamageBehaviour;
            }
        }
    }
    private void Update()
    {
        if (GameManager.CurrentState == GameState.Play)
        {
            Move?.Invoke();
            Damage?.Invoke();
        }
    }
}