using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth;
    Slider Healthbar;
    // Start is called before the first frame update
    void Start()
    {
        Healthbar = GetComponentInChildren<Slider>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.value = currentHealth;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;
    }

}
