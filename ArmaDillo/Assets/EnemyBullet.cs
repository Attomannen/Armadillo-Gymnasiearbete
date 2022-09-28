using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 10;
    public Rigidbody bullet;


    void Start()
    {
        bullet.velocity = transform.forward * bulletSpeed;
    }
}
