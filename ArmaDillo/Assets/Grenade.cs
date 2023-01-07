using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] GameObject FishExplosion;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionForce = 700f;
    [SerializeField] float explosionDamage = 25f;


    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider nearbyObjects in colliders)
        {
            Rigidbody rb = nearbyObjects.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce,transform.position, explosionRadius);
            }

        }

        EnemyHealth[] enemyHealth = FindObjectsOfType<EnemyHealth>();

        foreach(EnemyHealth health in enemyHealth)
        {
            if(Vector3.Distance(transform.position, health.transform.position) <= explosionRadius)
            {
                health.TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(FishExplosion, transform.position, Quaternion.identity);

        Explode();
    }
}
