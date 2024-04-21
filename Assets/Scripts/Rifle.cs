using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Rifle : Weapon
{
    public Transform barrel;
    public Transform origin;

    private WaitForSeconds timeToWait;
    private Vector2 bulletDirection;

    void Start()
    {
        timeToWait = new WaitForSeconds(1 / fireRate);
    }

    [ServerRpc]
    private void FireServerRPC()
    {
        bulletDirection = (barrel.position - origin.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, barrel.position, Quaternion.identity);

        Bullet b = bullet.GetComponent<Bullet>();
        b.velocity = bulletDirection;
        b.gunRange = range;

        bullet.GetComponent<NetworkObject>().Spawn();
    }

    public override void Fire()
    {
        StartCoroutine(FireCont());
    }

    private IEnumerator FireCont()
    {if(!IsOwner)
        {
            yield break;
        }
        do
        {
            FireServerRPC();

            yield return timeToWait;
        } while (HoldFire);
    }
}
