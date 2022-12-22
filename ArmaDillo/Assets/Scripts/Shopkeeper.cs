using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor.Rendering;

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
    [SerializeField] List<CinemachineVirtualCamera> shopCams;
    [SerializeField] int whichCam;


    // Start is called before the first frame update
    void Start()
    {
        interaction.enabled = false;

        anim = GetComponent<Animator>();

        dialogueManager = GetComponent<DialogueManager>();
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


        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            SwitchCam();
        }
     
    }
    bool hasInteracted;
    bool interact;
    [SerializeField] AudioClip helloClip;
    [SerializeField] AudioClip howAreYouClip;
    [SerializeField] AudioClip goodbyeClip;

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

            // assign the dialogue lines and audio clips to variables
            string[] lines = { "Hello!", "Welcome to my shop", "Would you want to take a look?" };
            AudioClip[] clips = { helloClip, howAreYouClip, goodbyeClip };

            // get a reference to the DialogueManager script
            DialogueManager dialogueManager = GetComponent<DialogueManager>();

            // start the dialogue by calling the StartDialogue method and passing in the lines and clips
            dialogueManager.StartDialogue(lines, clips);

        }
    }

    public void StopInteracting()
    {
        if (playerInRange && hasInteracted)
        {
            
            DialogueManager dialogueManager = GetComponent<DialogueManager>();
            dialogueManager.EndDialogue();
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
            shopCams[0].Priority = 0;
            shopCams[1].Priority = 0;
            shopCams[2].Priority = 0;

        }
    }
    private int currentActiveIndex = 0;
    DialogueManager dialogueManager;

    void SwitchCam()
    {


        shopCams[currentActiveIndex].Priority = 0;
        currentActiveIndex++;
        if (currentActiveIndex >= shopCams.Count)
            currentActiveIndex = 0;
        shopCams[currentActiveIndex].Priority = 20;


        if(currentActiveIndex == 0)
        {
            dialogueManager.EndDialogue();
            string[] lines = { "Drinkadillo", "Makes you faster and jump higher", "Buy enough to outrun the big crocodiles missiles" };
            AudioClip[] clips = { helloClip, howAreYouClip, goodbyeClip };


            // start the dialogue by calling the StartDialogue method and passing in the lines and clips
            dialogueManager.StartDialogue(lines, clips);
        }
        else if(currentActiveIndex == 1)
        {
            dialogueManager.EndDialogue();

            string[] lines = { "Drum Magazine", "Makes your Magazine have 5 more bullets per purchase", "But makes reloading slower" };
            AudioClip[] clips = { helloClip, howAreYouClip, goodbyeClip };

            // get a reference to the DialogueManager script

            // start the dialogue by calling the StartDialogue method and passing in the lines and clips
            dialogueManager.StartDialogue(lines, clips);
        }
        else if (currentActiveIndex == 2)
        {
            dialogueManager.EndDialogue();

            string[] lines = { "Lightning Ammo!", "Makes you shoot faster", "Both in bullet velocity but also trigger speed" };
            AudioClip[] clips = { helloClip, howAreYouClip, goodbyeClip };

            // get a reference to the DialogueManager script

            // start the dialogue by calling the StartDialogue method and passing in the lines and clips
            dialogueManager.StartDialogue(lines, clips);
        }
    }
   
}
