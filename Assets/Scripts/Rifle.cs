using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    public Transform barrel;
    public float speed;

    private WaitForSeconds timeToWait;

    void Start()
    {
        timeToWait = new WaitForSeconds(1 / fireRate);
    }
    IEnumerator ContinuousFire()
    {
        do
        {
            FireBullets();
            yield return timeToWait; 
        }while (RapidFire);
        yield return null;
    }
    private void FireBullets()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrel.position, transform.rotation);
        Bullet b = bullet.GetComponent<Bullet>();
        b.velocity = (barrel.position - transform.position).normalized * speed;
        b.timeToLive = range / speed;
    }
    public override void Damage()
    {
        StartCoroutine(nameof(ContinuousFire));
    }
}
