using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SwitchForms : MonoBehaviour
{
    [SerializeField] GameObject Normal;
    [SerializeField] GameObject Ball;
    //public bool isWerewolf;
    public int formSwitch { get; set; }
    PlayerInput input;
    InputAction modeAction;
    CharacterController controller;
    BallMovement ballScript;
    Movement normalMovement;
    private void Awake()
    {
        normalMovement = GetComponent<Movement>();
        ballScript = GetComponent<BallMovement>();
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        
        Normal.SetActive(true);
        Ball.SetActive(false);
        modeAction = input.actions["SwitchMode"];
        formSwitch = 1;
    }


    private void Update()
    {
        Transformation();
    }

    public void Transformation()
    {
        float action = modeAction.ReadValue<float>();
        if (action == 1)
        {
            BallArmadillo();
        }

        else if (action != 1)
        {
            NormalArmadillo();
        }
    }


    void NormalArmadillo()
    {
            Normal.SetActive(true);
            Ball.SetActive(false);
            formSwitch = 1;
            ballScript.enabled = false;
            normalMovement.enabled = true;
            Normal.transform.position = Ball.transform.position;
            controller.height = 1.5f;
    }

    void BallArmadillo()
    {
        Normal.SetActive(false);
        Ball.SetActive(true);
        formSwitch = 2;
        ballScript.enabled = true;
        normalMovement.enabled = false;
        Ball.transform.position = Normal.transform.position;
        Ball.transform.rotation = Normal.transform.rotation;
        controller.height = 1f;
    }
}