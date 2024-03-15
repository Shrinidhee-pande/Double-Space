using Unity.Netcode;
using UnityEngine;

public class Rifle : Weapon
{
    public Transform barrel;
    public Transform origin;
    public float bulletSpeedMultiplier;
 //   public float coolDown;

    private float timeToWait;
 //   private int capacity = 0;
    private float nextBulletTime = 0;
/*    private float coooldownTime = 0;
    private bool overheated = false;*/
    private Vector2 bulletDirection;

    void Start()
    {
        timeToWait = (1 / fireRate);
    }

    [ServerRpc]
    private void FireServerRPC()
    {
        bulletDirection = (origin.position-barrel.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, barrel.position, Quaternion.identity);

        Bullet b = bullet.GetComponent<Bullet>();
        b.velocity = bulletDirection * bulletSpeedMultiplier;
        b.timeToLive = range / bulletSpeedMultiplier;

        bullet.GetComponent<NetworkObject>().Spawn();
    }

    public override void Damage()
    {
        /*
        if (!overheated)
        {*/
        nextBulletTime -= Time.deltaTime;
        if (nextBulletTime < 0f)
        {
            if (HoldFire)
            {
                FireServerRPC();
                //capacity++;
                nextBulletTime = Time.deltaTime + timeToWait;
            }
        }
        /*            
            if (capacity >= maxCapacity)
            {
                capacity = 0;
                overheated = true;
                nextBulletTime = Time.deltaTime + coolDown;
            }
        }*/
    }
}
