using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public class Movement : MonoBehaviour
{
    PlayerInput playerInput;
    Vector2 input;

    public float moveSpeed;
    [SerializeField] float turnStrenght = 100f;
   [SerializeField] Transform turret;
    Rigidbody RB;
    float moveDirection;
    Vector2 rotationInput;

    Rumbler vibration;
    
    #region Region UnityMethods
    private void Awake()
    {
        //playerInput.DeactivateInput();
        Vector3 temp = new Vector3(0f, 0, 0);
        this.transform.position += temp; RB = GetComponent<Rigidbody>();
        RB.velocity = Vector3.zero;


        //playerControls.Disable();

        playerInput = GetComponent<PlayerInput>();

        vibration = GetComponent<Rumbler>();



    }

    private void Start()
    {
       playerInput.DeactivateInput();

    }


    public void ActivateInput()
    {
        playerInput.ActivateInput();
    }



    public void Update()
    {

        Rotate();

        if (input == Vector2.zero)
        {
            return;
        }


        float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;

        turret.rotation = Quaternion.Lerp(turret.rotation, Quaternion.Euler(0, targetAngle + 195, 0), 6f * Time.deltaTime);
        Debug.Log(moveDirection);

    }

    void FixedUpdate()
    {
        if (moveDirection < 0 || moveDirection > 0)
        {
            vibration.RumbleConstant(0.04f, 0.04f, 0.1f);
        }
        else
        {

            vibration.StopRumble();
        }
        Throttle();

   
    }

    #endregion
    #region Region Throttle/Rotations
    void Throttle()
    {
        RB.AddForce(transform.forward * moveDirection * moveSpeed, ForceMode.VelocityChange);

    }

  
    void Rotate()
    {


        if (rotationInput == Vector2.zero)
        {
            return;
        }


        float targetAngle = Mathf.Atan2(rotationInput.x, rotationInput.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetAngle + 195, 0), turnStrenght * Time.deltaTime);
    }




    public void Rotation(InputAction.CallbackContext context)
    {
        
        rotationInput = context.ReadValue<Vector2>();
    }

    public void RotationTurret(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void Throttling(InputAction.CallbackContext context)
    {

        moveDirection = context.ReadValue<float>();

      
    }




    #endregion
    #region Region CollisionDetection
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
        }

    }


    #endregion

 
}

