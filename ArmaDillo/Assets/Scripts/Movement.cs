using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -39.81f;
    [SerializeField] float rotationSpeed = 5f;
    float normalSpeed = 12;
    float ballSpeed = 20;

    PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction runningAction;

    private Transform camTransform;
    float inputSpeed;
    Animator anim;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        anim = GetComponent<Animator>();
        camTransform = Camera.main.transform;
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        moveAction = input.actions["Move"];

        jumpAction = input.actions["Jump"];
        runningAction = input.actions["SwitchMode"];


    }
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    [SerializeField] float smoothInputSpeed = 0.5f;


    void Update()
    {
        groundedPlayer = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
        anim.SetFloat("Blend", currentInputVector.magnitude, 1, 1);
        Vector3 move = new Vector3(currentInputVector.x * 0.7f, 0, currentInputVector.y);





        move = move.x * camTransform.right.normalized + move.z * camTransform.forward.normalized;

        move.y = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);







        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            anim.SetTrigger("Jump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        float targetAngle = camTransform.rotation.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        inputSpeed = runningAction.ReadValue<float>();

        if(inputSpeed > 0f)
        {
            playerSpeed = ballSpeed;
            virtualCamera.m_Lens.FieldOfView = 45;
        }
        else
        {
            playerSpeed = normalSpeed;
            virtualCamera.m_Lens.FieldOfView = 40;

        }

    }

}




