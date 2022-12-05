using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    int playersTotal;

    [SerializeField] TankHealth[] players = new TankHealth[0];


    //private void Update()
    //{
    //    if (players.Length < 2)
    //    {
    //        players = FindObjectsOfType<TankHealth>();
    //    }
    //}

    //private void LateUpdate()
    //{
    //    for (int i = 0; i < players.Length; i++)
    //    {
    //        if (players[i].GetComponent<Movement>() != null) { return; }

    //        if (players[i].gameObject.name == "Player(Clone)")
    //        {
    //        }
    //        else
    //        {
    //            SceneManager.LoadScene(3);
    //        }
    //    }
    //}



    private void Update()
    {
        if (players.Length < 2)
        {
         players = FindObjectsOfType<TankHealth>();

        }
   
    
    }

    public void PolandWins()
    {
        SceneManager.LoadScene(2);

    }

    public void SwedenWins()
    {
        SceneManager.LoadScene(3);

    }


}
