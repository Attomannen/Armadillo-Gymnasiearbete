using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform player; // the player's Transform

    public float range = 10f; // the maximum range at which the enemy can shoot the player

    void Update()
    {
        // shoot a Raycast to check if the player is within range
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            // if the player is within range
            if (hit.collider.tag == "Player")
            {
                // log the hit information to the console
                Debug.Log("Enemy hit: " + hit.collider.name + " at position " + hit.point);
            }
        }
    }
}