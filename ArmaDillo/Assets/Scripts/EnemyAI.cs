using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // The layer mask for the cover objects
    public LayerMask coverMask;

    // The distance at which the enemy will start seeking cover
    public float seekCoverDistance = 10f;

    // The speed at which the enemy moves
    public float moveSpeed = 5f;

    // The NavMeshAgent component attached to the enemy
    public NavMeshAgent agent;

    // The transform of the enemy's target (e.g. the player)
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance to the target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // If the distance to the target is less than the seek cover distance, seek cover
        if (distanceToTarget < seekCoverDistance)
        {
            // Find the nearest cover object using the cover mask
            RaycastHit hit;
            if (Physics.Raycast(transform.position, target.position - transform.position, out hit, seekCoverDistance, coverMask))
            {
                // Set the destination of the NavMeshAgent to the position of the cover object
                agent.destination = hit.transform.position;
            }
        }
    }
}