using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUpTurret : MonoBehaviour
{
    [SerializeField] GameObject realTurret;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);

        realTurret.SetActive(false);
        gameObject.AddComponent<Rigidbody>();
        
        rb = gameObject.GetComponent<Rigidbody>();

        rb.AddForce(transform.up * 25f, ForceMode.Impulse);


    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(2, 2, 2));
    }
}
