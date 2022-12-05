using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem stoneParticle;
    [SerializeField] ParticleSystem bulletParticle;
    [SerializeField] ParticleSystem houseParticle;
    [SerializeField] LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
    [SerializeField] AudioSource explosionAudio;         // Reference to the particles that will play on explosion.
    [SerializeField] ParticleSystem m_Particles;
    [SerializeField] AudioSource impactAudio;                // Reference to the audio that will play on explosion.
    [SerializeField] float m_MaxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
    [SerializeField] float m_ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
    [SerializeField] float m_ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.
    int HouseHealth;
    CameraShake shake;
    private void Awake()
    {
        shake = Camera.main.GetComponent<CameraShake>();

        explosionAudio.Play();
    }

    private void OnCollisionEnter(Collision other)
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
        if(other.gameObject.tag != "Player")
        {
        shake.StartCoroutine(shake.StartNoise(2f,2f));

        }

        Instantiate(impactAudio);
        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(m_Particles, pos, rot);
        Destroy(gameObject);


        if(other.gameObject.tag == "Bullet")
        {
            Instantiate(impactAudio);
            Instantiate(bulletParticle, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
        }


        if (other.gameObject.tag == "House")
        {


       
            Instantiate(houseParticle, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);
            

        }
        if (other.gameObject.tag == "Stone")
        {



            Instantiate(stoneParticle, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);


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
        damage = Mathf.Max(0f, damage);

        return damage;
    }
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, m_ExplosionRadius);
    }

}
