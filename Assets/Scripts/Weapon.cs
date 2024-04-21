using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Weapon : NetworkBehaviour
{
    public float fireRate;
    public float range;
    public int maxCapacity;
    public GameObject bulletPrefab;
    public bool IsActive { get; set; }
    public bool HoldFire { get; set; }

    public abstract void Fire();
}
