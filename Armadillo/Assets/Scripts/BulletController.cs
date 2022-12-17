using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(transform.forward * 2f);
    }
}
