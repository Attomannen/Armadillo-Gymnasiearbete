using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{




    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);


        }
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {

            foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            child.GetComponent<Rigidbody>().AddForce(Random.Range(-5,20), Random.Range(0,10),Random.Range(-5,20), ForceMode.Impulse);
            child.GetComponent<Rigidbody>().AddTorque(Random.Range(-5, 20), Random.Range(0, 10), Random.Range(-5, 20), ForceMode.Impulse);

        }
        }


    }

}
