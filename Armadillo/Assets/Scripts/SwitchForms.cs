using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SwitchForms : MonoBehaviour
{

    //public bool isWerewolf;
    public int formSwitch { get; set; }
    PlayerInput input;
    InputAction modeAction;
    CharacterController controller;
    Movement movement;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();

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
        anim.SetBool("Ball", false);

        //controller.height = 2f;
    }

    void BallArmadillo()
    {
        anim.SetBool("Ball", true);

        //controller.height = 1f;
    }
}