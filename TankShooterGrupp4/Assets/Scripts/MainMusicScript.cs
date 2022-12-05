using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMusicScript : MonoBehaviour
{
   // [SerializeField] bool activateCoreMusic;
   // [SerializeField] bool activateMainMenuMusic;
    [SerializeField] AudioSource musicDestination;
    [SerializeField] AudioSource mainMenuMusic;
    [SerializeField] AudioSource swedenNationalAnthem;

    [SerializeField, Range(0, 1)] float volumeAmount;
    [SerializeField, Range(0, 1)] float mainMenuVolumeAmount;


    private void Awake()
    {

       
        if (SceneManager.GetActiveScene().name == "CoreScene")
        {
            musicDestination.Play();
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            mainMenuMusic.Play();
        }
        if(SceneManager.GetActiveScene().name == "SwedenWin")
        {
            swedenNationalAnthem.Play();
        }
      
    }

    private void Update()
    {
        
       
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {

            musicDestination.Stop();

        }
        if (SceneManager.GetActiveScene().name == "CoreScene")
        {
            mainMenuMusic.Stop();
        }
        //if (activateCoreMusic)
        //{
        //    musicDestination.Play();
        //}
        //if (!activateCoreMusic)
        //{
        //    musicDestination.Pause();
        //}
        //if (activateMainMenuMusic)
        //{
        //    mainMenuMusic.Play();
        //}
        //if (!activateMainMenuMusic)
        //{
        //    mainMenuMusic.Pause();
        //}
        musicDestination.volume = volumeAmount;
        mainMenuMusic.volume = mainMenuVolumeAmount;
    }
}
