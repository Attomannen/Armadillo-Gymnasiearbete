using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManagement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pressToJoinText;
   [SerializeField] GameObject textObject;
   [SerializeField] TextMeshProUGUI text;
    int index = 0;
    [SerializeField] List<GameObject> tanks = new List<GameObject>();
   PlayerInputManager manager;
    GameObject player;
    GameObject player2;
    [SerializeField] float countDown = 1;
    [SerializeField] RawImage background;
    bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Number of Players: " + index;
        manager = GetComponent<PlayerInputManager>();
        index = 0;
        manager.playerPrefab = tanks[index];
        manager.EnableJoining();

    }




    public void SwitchTankOnCharacterSpawn(PlayerInput input)
    {

        index++;
        manager.playerPrefab = tanks[index];
        if(text != null)
        {
            text.text = "Number of Players: " + index;

        }

    }


    private void Update()
    {

        if (index == 2)
        {
            pressToJoinText.text = "Poland vs Sweden";
            player = GameObject.Find("Player(Clone)");
            player2 = GameObject.Find("Player2(Clone)");
            background.enabled = false;
            countDown -= Time.deltaTime;
            if(countDown != 0)
            {
                text.text = "" + (int)countDown;

            }
            else
            {
       

            }
            if (player2 && !hasStarted)
            {
                StartCoroutine(StartGame());

            }

            manager.DisableJoining();
            manager.playerPrefab = null;

            if(hasStarted && player == null)
            {
                SceneManager.LoadScene(2);
            }
            if (hasStarted && player2 == null)
            {
                SceneManager.LoadScene(3);
            }

        }
    }


    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(countDown);


        player = GameObject.Find("Player(Clone)");
        player2 = GameObject.Find("Player2(Clone)");
        yield return new WaitForSeconds(0.1f);
        hasStarted = true;
        player.GetComponent<PlayerInput>().ActivateInput();
        player2.GetComponent<PlayerInput>().ActivateInput();
        textObject.SetActive(false);
        StopCoroutine(StartGame());
    }


   
}
