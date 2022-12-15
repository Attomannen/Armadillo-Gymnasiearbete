using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArneParticleHit : MonoBehaviour
{


    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(25);

        }
    }
}
