using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float fireSpeed;

    private Weapon weapon;

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
    }
    void Update()
    {
        weapon.Fire();
    }
}
