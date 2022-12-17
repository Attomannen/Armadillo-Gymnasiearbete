using UnityEngine;
using UnityEngine.AI;

public class EnemyShootAI : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    public Transform Player;
    public LayerMask GroundCheck, PlayerCheck;
    public Transform EnemyPrototype;
    public float range;
    public float Damage;

    public Vector3 WalkPoint; // Code for patrolling Around
    bool WalkPointSet;
    public float WalkPointRange;

    public float SightRange, AttackRange;   // Code for checking when to engage in a firefight with the player
    public bool PlayerInSight, AttackPlayer;

    public float AttackCooldown; // Code for firefights with the player
    bool AlreadyAttacked;

    private void AIActive()
    {
        Player = GameObject.Find("Player").transform;
        NavAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        PlayerInSight = Physics.CheckSphere(transform.position, SightRange, PlayerCheck);
        AttackPlayer = Physics.CheckSphere(transform.position, AttackRange, PlayerCheck);

        if (!PlayerInSight && !AttackPlayer)
        {
            Patrol();
        }
        if (PlayerInSight && !AttackPlayer)
        {
            Engage();
        }
        if (PlayerInSight && AttackPlayer)
        {
            FireAtPlayer();
        }
    }

    private void Patrol() // The enemy will move in order to try and find the player
    {
        if (!WalkPointSet)
        {
            SearchWalkPoint();
        }

        if (WalkPointSet)
        {
            NavAgent.SetDestination(WalkPoint);
        }

        Vector3 WalkPointDistance = transform.position - WalkPoint;

        if (WalkPointDistance.magnitude < 1f)
        {
            WalkPointSet = false;
        }
    }

    private void SearchWalkPoint()  // Makes a random point where the enemy would patrol
    {
        float WalkZ = Random.Range(-WalkPointRange, WalkPointRange);
        float WalkX = Random.Range(-WalkPointRange, WalkPointRange);
        WalkPoint = new Vector3(transform.position.x + WalkX, transform.position.y, transform.position.z + WalkZ);
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, GroundCheck))
        {
            WalkPointSet = true;
        }
    }

    private void Engage() // The enemy has found the player and will move towards them to get a better shot
    {
        NavAgent.SetDestination(Player.position);
    }

    private void FireAtPlayer() // The enemy will now try to get a shot on the player
    {
        NavAgent.SetDestination(transform.position);
        transform.LookAt(Player);
        if (!AlreadyAttacked)
        {
            RaycastHit Hit;
            if (Physics.Raycast(EnemyPrototype.transform.position, EnemyPrototype.transform.forward, out Hit, range, PlayerCheck))
            {
                Debug.Log(Hit.collider.name);

                PlayerHealth target = Hit.transform.GetComponent<PlayerHealth>();
                if (target != null)
                {
                    target.TakeDamage(Damage);
                    Debug.Log("Meow");
                }
            }
            AlreadyAttacked = true;
            Invoke(nameof(AttackReset), AttackCooldown);
        }
    }

    private void AttackReset()
    {
        AlreadyAttacked = false;
    }
}
