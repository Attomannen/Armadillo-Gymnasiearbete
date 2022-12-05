using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class TankHealth : MonoBehaviour
{

    [SerializeField] GameObject particleEffect;
    [SerializeField] ParticleSystem healthParticle;
    [SerializeField] GameObject tankTop;
    [SerializeField] GameObject turretForDeath;
    [SerializeField] Image healthBar;
    [SerializeField] float maxHealth;
    float decreaseSpeed;
    public float currentHealth;
    Rigidbody rb;
    CameraShake shake;

    void Start()
    {
        turretForDeath.SetActive(false);
        shake = Camera.main.GetComponent<CameraShake>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        decreaseSpeed = 3f * Time.deltaTime;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, decreaseSpeed);

        if (currentHealth <= 0)
        {
            StartCoroutine(DestroyTank());

        }




        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Health")
        {
            Instantiate(healthParticle, collision.transform.position, Quaternion.identity);
            currentHealth += 45f;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "MysteryHealth")
        {
            Instantiate(healthParticle, collision.transform.position, Quaternion.identity);
            currentHealth += Random.Range(-100f, 100f);
            Destroy(collision.gameObject);
        }
    }


    public void TakeDamage(float damage)
    {
        shake.StartCoroutine(shake.StartNoise(7,15));
        currentHealth -= damage;
    }


    public IEnumerator DestroyTank()
    {
        gameObject.GetComponent<PlayerInput>().DeactivateInput();



        turretForDeath.SetActive(true);
        particleEffect.SetActive(true);
        yield return new WaitForSeconds(2);
        

        Destroy(gameObject);


        StopCoroutine(DestroyTank());
    }
}
