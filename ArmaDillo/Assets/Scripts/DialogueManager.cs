using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // a UI text element to display the dialogue lines
    public AudioSource audioSource; // an AudioSource component to play the audio clips
    public AudioClip[] dialogueClips; // an array of audio clips, one for each line of dialogue

    public string[] dialogueLines; // an array of strings to hold the dialogue lines

    public int currentLine; // a variable to track the current line of dialogue

    public bool isDialogueActive; // a boolean to track whether the dialogue is currently active

    public GameObject dialoguePanel; // a reference to the dialogue panel GameObject

    // Start is called before the first frame update
    void Start()
    {
        // disable the dialogue panel at the start of the game
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // check if the dialogue is active and the player has pressed the "next" button
        if (isDialogueActive && Keyboard.current.spaceKey.wasPressedThisFrame)
        {

            
            
            // advance to the next line of dialogue
            currentLine++;

            // check if we have reached the end of the dialogue
            if (currentLine >= dialogueLines.Length)
            {
                // end the dialogue
                EndDialogue();
            }
            else
            {
                // display the next line of dialogue and play the corresponding audio clip
                dialogueText.text = dialogueLines[currentLine];
                audioSource.clip = dialogueClips[currentLine];
                audioSource.Play();
            }
        }

        Animator anim = GetComponent<Animator>();

        anim.SetBool("IsTalking", audioSource.isPlaying);
    }

    // a method to start the dialogue
    public void StartDialogue(string[] lines, AudioClip[] clips)
    {
        // set the dialogue lines and audio clips, and enable the dialogue panel
        dialogueLines = lines;
        dialogueClips = clips;
        dialoguePanel.SetActive(true);

        // display the first line of dialogue and play the corresponding audio clip
        dialogueText.text = dialogueLines[currentLine];
        audioSource.clip = dialogueClips[currentLine];
        audioSource.Play();

        // set the isDialogueActive flag to true
        isDialogueActive = true;
    }

    // a method to end the dialogue
    public void EndDialogue()
    {
        // stop the audio, disable the dialogue panel, and set the isDialogueActive flag to false
        audioSource.Stop();
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        // reset the current line of dialogue
        currentLine = 0;
    }
}
