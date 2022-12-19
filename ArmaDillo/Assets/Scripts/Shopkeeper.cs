using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UIElements;

public class Shopkeeper : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] Transform player;
    Animator anim;
    [SerializeField] TextMeshProUGUI interaction;
    [SerializeField] GameObject crossHair;
    [SerializeField] GameObject crossHair2;
    [SerializeField] Movement movement;
    [SerializeField] SwitchForms switchForms;
    [SerializeField] PlayerHealth health;
    [SerializeField] PlayerGun gun;
    // Start is called before the first frame update
    void Start()
    {
        interaction.enabled = false;

        anim = GetComponent<Animator>();
    }

    bool playerInRange;
    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 15);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.tag == "Player" && !hasInteracted)
            {
            interaction.enabled = true;
            playerInRange = true;
            }
        }
    }
    bool hasInteracted;
    bool interact;

    public void interactBool()
    {
        interact = true;
    }
    public void stopInteractBool()
    {
        interact = false;
    }
    public void Interact()
    {
        if(playerInRange && !hasInteracted)
        {
            interactBool();
            gun.enabled = false;
            movement.enabled = false;
            switchForms.enabled = false;
            health.enabled = false;
            hasInteracted = true;
            crossHair2.SetActive(false); 
            crossHair.SetActive(false);
            interaction.enabled = false;
            anim.SetBool("Standing", true);
            cam.Priority = 20;
        }
    }

    public void StopInteracting()
    {
        if (playerInRange && hasInteracted)
        {
            stopInteractBool();
            gun.enabled = true;
            movement.enabled = true;
            switchForms.enabled = true;
            health.enabled = true;
            hasInteracted = false;
            crossHair2.SetActive(true);
            crossHair.SetActive(true);
            interaction.enabled = true;
            anim.SetBool("Standing", false);
            cam.Priority = 0;
        }
    }
  
}
