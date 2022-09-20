using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 10;
    public Rigidbody bullet;


    void Start()
    {
        Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.velocity = transform.forward * bulletSpeed;
    }
}
