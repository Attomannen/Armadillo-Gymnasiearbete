using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField] GameObject particle;

    [SerializeField] LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
    [SerializeField] float m_MaxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
    [SerializeField] float m_ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
    [SerializeField] float m_ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.
    [SerializeField] GameObject barrelExplosionSound;



    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            // Add an explosion force.
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;

            // Calculate the amount of damage the target should take based on it's distance from the shell.
            float damage = CalculateDamage(targetRigidbody.position);
            // Deal this damage to the tank.
            targetHealth.TakeDamage(damage);


        }



        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {

            

        Instantiate(barrelExplosionSound);

            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(particle, pos, rot);
            Destroy(gameObject);

        }
    }




    private float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(10f, damage);

        return damage;
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, m_ExplosionRadius);
    }





}
