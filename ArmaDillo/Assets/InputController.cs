using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    PlayerInput input;
    InputAction shoot;

    PlayerGun gun;
    private void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
        gun = GetComponent<PlayerGun>();
        input = GetComponent<PlayerInput>();
        shoot = input.actions["Fire"];

    }
    // Update is called once per frame
    void Update()
    {
        if (shoot.triggered)
        {
        gun.Shoot();
        }

    }
}
