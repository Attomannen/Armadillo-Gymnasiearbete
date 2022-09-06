using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchForms : MonoBehaviour
{
    [SerializeField] GameObject Normal;
    [SerializeField] GameObject Ball;
    //public bool isWerewolf;
    [SerializeField] int formSwitch;
    BallMovement ballScript;
    NormalMovement normalMovement;
    private void Awake()
    {
        normalMovement = GetComponent<NormalMovement>();
        ballScript = GetComponent<BallMovement>();
        Normal.SetActive(true);
        Ball.SetActive(false);
        ballScript.enabled = false;

        formSwitch = 1;
    }


    private void Update()
    {
        Transformation();
    }

    public void Transformation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Normal.SetActive(false);
            Ball.SetActive(true);
            formSwitch = 2;
            ballScript.enabled = true;
            normalMovement.enabled = false;
            Ball.transform.position = Normal.transform.position;
            Ball.transform.rotation = Normal.transform.rotation;
            
        }

        else if (Input.GetMouseButtonUp(1))
        {
            Normal.SetActive(true);
            Ball.SetActive(false);
            formSwitch = 1;
            ballScript.enabled = false;
            normalMovement.enabled = enabled;
            Normal.transform.position = Ball.transform.position;
            Normal.transform.rotation = Ball.transform.rotation;


        }
    }
}