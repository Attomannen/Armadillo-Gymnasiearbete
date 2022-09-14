using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BallMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed = 5f;
    PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;
    private Transform camTransform;

    private void Start()
    {
        camTransform = Camera.main.transform;
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];

    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * camTransform.right.normalized + move.z * camTransform.forward.normalized;
        move.y = 0f;


        controller.Move(move * Time.deltaTime * playerSpeed);



        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        float targetAngle = camTransform.rotation.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

    }
}