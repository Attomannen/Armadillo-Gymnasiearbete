using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotToAngle : MonoBehaviour
{
    Vector2 input;

    void Update()
    {
        if (input == Vector2.zero)
        {
            return;
        }

        Vector3 targetRotation = new Vector3(input.x, 0, input.y);

        //Vector3 rotation = Vector3.Lerp(transform.position, targetRotation, 1 * Time.deltaTime);

        //transform.position = rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetRotation), 10f * Time.deltaTime);
    }

    public void RotationTurret(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
