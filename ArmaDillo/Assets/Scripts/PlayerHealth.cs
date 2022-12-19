using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;
    [SerializeField] Slider healthImage;
    [SerializeField] TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.value = currentHealth;
        healthText.text = "HP: " + currentHealth;
        if (currentHealth <= 0)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);

        }
    }
   public void TakeDamage(float Damage)
    {
        Debug.Log("Taken Damage");
        currentHealth -= Damage;
    }
 
}
