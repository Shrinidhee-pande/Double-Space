using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float fireRate;
    public float range;
    public float capacity;
    public GameObject bulletPrefab;
    public bool IsActive { get; set; }
    public bool HoldFire { get; set; }

    public abstract void Damage();
}
