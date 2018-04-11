using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    public int damage = 30;
    public int ammo = 1;
    public int maxAmmo = 99;
    public LayerMask whatToHit;

    public static void Shoot(Turret turret, GameObject bulletPrefab, Transform barrelTip, float power)
    {
        Debug.Log("fire 2");
        GameObject clone = Instantiate(bulletPrefab, barrelTip.transform.position, barrelTip.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * -power);
        Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), turret.GetComponent<Collider2D>());
        Destroy(clone, 10);
    }
}