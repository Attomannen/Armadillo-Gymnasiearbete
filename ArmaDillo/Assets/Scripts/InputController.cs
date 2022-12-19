using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    PlayerInput input;
    InputAction shoot;
    InputAction interact;

    PlayerGun gun;
    [SerializeField] Shopkeeper shop;
    private void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
        gun = GetComponent<PlayerGun>();
        input = GetComponent<PlayerInput>();
        shoot = input.actions["Fire"];
        interact = input.actions["Interaction"];
    }
    // Update is called once per frame

    bool ifInteracted;
    void Update()
    {
        if (shoot.triggered && !ifInteracted)
        {
        gun.Shoot();
        }
        if(interact.triggered && !ifInteracted)
        {
        shop.Interact();
            ifInteracted = true;
        }
        else if(interact.triggered && ifInteracted)
        {
         shop.StopInteracting();
         ifInteracted = false;
        }



    }
}
