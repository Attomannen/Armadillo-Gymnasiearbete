using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] GameObject PolandWinScreen;
    [SerializeField] GameObject SwedenWinScreen;
    
    void Start()
    {
        if (PolandWinScreen == true)
        {
            SwedenWinScreen.SetActive(false);
        }
        
    }

    // Update is called once per frame 
    void Update()
    {
        
    }
}
