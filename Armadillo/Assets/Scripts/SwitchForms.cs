using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class SwitchForms : MonoBehaviour
{

    //public bool isWerewolf;
    [SerializeField] int formSwitch { get; set; }
    PlayerInput input;
    InputAction modeAction;
    CharacterController controller;
    Movement movement;
    Animator anim;
    PlayerGun shoot;
   [SerializeField] Image crosshair1;
    [SerializeField] Image crosshair2;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        shoot = GetComponent<PlayerGun>();
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
        crosshair1.enabled = true; crosshair2.enabled = true;
        shoot.enabled = true;
        anim.SetBool("Ball", false);
        //controller.height = 2f;
    }

    void BallArmadillo()
    {
        crosshair1.enabled = false; crosshair2.enabled = false;

        shoot.enabled = false;
        anim.SetBool("Ball", true);

        //controller.height = 1f;
    }
}