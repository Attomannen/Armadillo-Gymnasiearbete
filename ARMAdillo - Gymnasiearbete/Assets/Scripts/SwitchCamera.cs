using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class SwitchCamera : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    private InputAction aimAction;
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] int priorityBoost = 10;

    [SerializeField] Canvas aimCanvas;
    [SerializeField] Canvas normalCanvas;


    // Start is called before the first frame update
    void Awake()
    {
        aimCanvas.enabled = false;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"]; 
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }



    private void OnDisable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }


    void StartAim()
    {
        virtualCamera.Priority += priorityBoost;
        aimCanvas.enabled = true;
        normalCanvas.enabled = false;
    }

    void CancelAim()
    {
        virtualCamera.Priority -= priorityBoost;
        aimCanvas.enabled = false;
        normalCanvas.enabled = true;
    }

}
