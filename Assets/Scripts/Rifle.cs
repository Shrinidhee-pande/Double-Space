using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    public Transform[] barrels; 
    public Transform[] origins; 
    public float bulletSpeedMultiplier;

    private WaitForSeconds timeToWait;
    private Vector2[] bulletDirections;
    void Start()
    {
        timeToWait = new WaitForSeconds(1 / fireRate);
        bulletDirections = new Vector2[barrels.Length];
        for (int i = 0; i < barrels.Length; i++)
        {
            bulletDirections[i] = (barrels[i].position - origins[i].position).normalized;
        }
    }
    IEnumerator ContinuousFire()
    {
        while (HoldFire)
        {
            Fire();
            yield return timeToWait; 
        }
        yield return null;
    }
    private void Fire()
    {
        for (int i = 0; i < barrels.Length; i++)
        {
            bulletDirections[i] = (barrels[i].position - origins[i].position).normalized;
        }
        for (int i = 0; i < barrels.Length; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, barrels[i].position, transform.rotation);
            Bullet b = bullet.GetComponent<Bullet>();
            b.velocity = bulletDirections[i] * bulletSpeedMultiplier;
            b.timeToLive = range / bulletSpeedMultiplier;
        }
    }
    public override void Damage()
    {
        if (HoldFire)
        {
            StartCoroutine(nameof(ContinuousFire));
        }
        else
        {
            Fire();
        }
    }
}
