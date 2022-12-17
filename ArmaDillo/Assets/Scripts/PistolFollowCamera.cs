using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PistolFollowCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(Camera.main.transform.rotation.x * 125, transform.localRotation.y, transform.localRotation.z);

    }
}
