using Cinemachine;
using UnityEngine;

public enum EnemyState { OutOfRange, InRange, TooClose };

public class EnemyBrain : MonoBehaviour 
{
    public Transform player;
    public EnemyState state;

    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;

    private EnemyDamageBehaviour damageBehaviour;
    private EnemyMovementBehaviour movementBehaviour;

    void Start()
    {
        damageBehaviour = GetComponent<EnemyDamageBehaviour>();
        movementBehaviour = GetComponent<EnemyMovementBehaviour>();
        player = FindObjectOfType<PlayerSpecification>().transform;
    }
    private void Update()
    {
        if (GameManager.CurrentState == GameState.Play)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance > maxRange)
            {
                state = EnemyState.OutOfRange;
            }
            else if (distance > minRange)
            {
                state = EnemyState.InRange;
            }
            else
            {
                state = EnemyState.TooClose;
            }
            movementBehaviour.Move();
        }
    }
}