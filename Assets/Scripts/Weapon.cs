using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public float range;
    public GameObject bulletPrefab;
    public bool rapidFire { get; set; }
     

    public Weapon()
    {
        damage = 10f;
        fireRate = 3f;
        range = 5f;
        bulletPrefab = null;
    }
    public abstract void Damage();
}
