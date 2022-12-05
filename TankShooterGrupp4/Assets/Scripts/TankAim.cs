using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
public class TankAim : MonoBehaviour
{
    
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem turretFlare;
    [SerializeField] GameObject shot;
    [SerializeField] Transform shotpoint;
    [SerializeField] RawImage UIFill;
    [SerializeField] Slider UI;
    [SerializeField] float bulletKnockback = 100f;
    Rigidbody rb;
    float timeElapsed;
    float lerpDuration = 0.6f;
    float minDamage = 0;
    float maxDamage = 100;
    float damage;
    float shooting;
    bool isShooting;

    
    float bulletSpeed;

    [SerializeField] float bulletProjectile;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bulletSpeed = damage * bulletProjectile;

        UpdateSlider();
    }

    void Shoot()
    {
        if (damage > 20)
        {

            animator.SetTrigger("Shoot");
            turretFlare.Play();

            Vector3 knockbackDirection = new Vector3(shotpoint.position.x - transform.position.x, 0, shotpoint.position.z - transform.position.z).normalized;
            rb.AddForce(knockbackDirection * damage * -bulletKnockback * 2);

            GameObject shotInstance = Instantiate(shot, shotpoint.position, shotpoint.rotation);
            shotInstance.GetComponent<Rigidbody>().AddForce(shotpoint.forward * bulletSpeed * 2,ForceMode.Impulse);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
          //  rb.AddForce(transform.forward * -bulletKnockback,ForceMode.Impulse);
        }
    }

   public void Shooting(InputAction.CallbackContext context)
    {
       shooting = context.ReadValue<float>();

        if(shooting == 1)
        {

            isShooting = true;
        }
        else
        {
            timeElapsed = 0;
            isShooting = false;
        }
    }

    void UpdateSlider()
    {
        if (isShooting && timeElapsed < lerpDuration + 0.01f)
        {
            UI.GetComponentInParent<Canvas>().enabled = true;
            damage = Mathf.Lerp(minDamage, maxDamage, timeElapsed / lerpDuration);
            UIFill.color = Color.Lerp(Color.red, Color.green, timeElapsed / lerpDuration);

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= lerpDuration + 0.01f)
            {
                Shoot();

                damage = 0;
            }
        }
        else if (isShooting != true)
        {
            Shoot();

            timeElapsed = 0;

            damage = 0;

        }
        
        UI.value = damage;
    }
}
