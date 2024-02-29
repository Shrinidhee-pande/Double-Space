using UnityEngine;

[RequireComponent(typeof(EnemyBrain))]
public abstract class EnemyDamageBehaviour : MonoBehaviour
{
    public float damageValue;
    public float collisionDamage;

    public abstract void Damage();
    public abstract void SpecialDamage();
}
