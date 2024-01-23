using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    public Transform barrel;
    public float holdDuration = 0;
    public float speed;
    private WaitForSeconds timeToWait;
    Rifle()
    {
        damage = 10f;
        fireRate = 5f;
        range = 100f;
        speed = 25f;
    }
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
        }while (rapidFire);
        yield return null;
    }
    private void FireBullets()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrel.position, Quaternion.identity);
        Bullet b = bullet.AddComponent<Bullet>();
        b.direction = new Vector2(barrel.position.x - transform.position.x, barrel.position.y - transform.position.y).normalized * speed;
        b.timeToLive = range / speed;
        Debug.Log("Direction : " + b.direction.ToString()+ ",  ttl : "+ b.timeToLive);
    }

    public override void Damage()
    {
        StartCoroutine(nameof(ContinuousFire));
    }
}
