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
    [SerializeField] int formSwitch;
    PlayerInput input;
    InputAction modeAction;
    CharacterController controller;
    BallMovement ballScript;
    NormalMovement normalMovement;
    private void Awake()
    {
        normalMovement = GetComponent<NormalMovement>();
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
            Normal.SetActive(false);
            Ball.SetActive(true);
            formSwitch = 2;
            ballScript.enabled = true;
            normalMovement.enabled = false;

            Ball.transform.position = Normal.transform.position;
            Ball.transform.rotation = Normal.transform.rotation;
            controller.height = 1f;

        }

        else if (action != 1)
        {
            Normal.SetActive(true);
            Ball.SetActive(false);
            formSwitch = 1;
            ballScript.enabled = false;
            normalMovement.enabled = true;

            Normal.transform.position = Ball.transform.position;
            controller.height = 2f;

        }
    }
}