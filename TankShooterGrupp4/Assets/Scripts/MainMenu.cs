using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    EventSystem eventSystem;

    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject firstSelectedMain;
    [SerializeField] GameObject firstSelectedOptions;

    private void Awake()
    {
        eventSystem = EventSystem.current;
    }

    // Start is called before the first frame update
    public void PlayGame()
    {

        SceneManager.LoadScene("CoreScene");
    }

    // Update is called once per frame
    public  void QuitGame()
    {

        Debug.Log("QUIT");
        Application.Quit();
    }

    public void BackButton()
    {
        optionsScreen.SetActive(false);
        mainScreen.SetActive(true);

        eventSystem.SetSelectedGameObject(firstSelectedMain);
    }

    public void Options()
    {
        optionsScreen.SetActive(true);
        mainScreen.SetActive(false);

        eventSystem.SetSelectedGameObject(firstSelectedOptions);
    }
    public void LoadMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }
}
