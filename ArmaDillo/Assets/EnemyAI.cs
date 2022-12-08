using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // the player's Transform

    public Transform[] patrolPoints; // an array of points to patrol
    private int currentPatrolPoint = 0; // the index of the current patrol point

    public Transform coverPoint; // the position of cover to move to when the player shoots

    private NavMeshAgent nav; // the NavMeshAgent component

    void Start()
    {
        // get the NavMeshAgent component
        nav = GetComponent<NavMeshAgent>();
    }
    bool takeCover;
    float coverTimer;
    [SerializeField] float rangeOfPlayer;
    [SerializeField] float patrolRadius = 20;
    float timer;
    void Update()
    {
        // if the enemy is in cover
        if (takeCover)
        {
            // increment the cover timer
            coverTimer += Time.deltaTime;

            // if the enemy has been in cover for a certain amount of time
            if (coverTimer >= 5f)
            {
                nav.speed = 3.5f;
                coverTimer = 0;
                // move the enemy out of cover
                nav.SetDestination(player.position);
                takeCover = false;
            }
        }
        // if the enemy is within a certain distance of the player
        if (Vector3.Distance(transform.position, player.position) < rangeOfPlayer && !takeCover)
        {
            // set the enemy's destination to the player's position
            Vector3 offsetPosition = Vector3.MoveTowards(player.position, transform.position, 5);
            nav.SetDestination(offsetPosition);

        }
        else
        {
            // set the enemy's destination to the next patrol point
            if (!takeCover)
            {
                timer += Time.deltaTime;
                randomPos = randomPosition;
                if (timer >= 5f)
                {
                    StartCoroutine(RandomPos());
                    nav.SetDestination(lastRandomPos);
                    timer = 0;
                    if(nav.transform.position == lastRandomPos)
                    {
                        StartCoroutine(RandomPos());
                    }
                }
            }

            // if the enemy has reached the patrol point, move to the next one
            if (nav.remainingDistance < nav.stoppingDistance)
            {
                currentPatrolPoint++;

                // if the enemy has reached the last patrol point, start over at the first one
                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }
            }
        }

    
    }
    Vector3 randomPos;
    Vector3 randomPosition;
    Vector3 lastRandomPos;
    IEnumerator RandomPos()
    {
        randomPosition = transform.position + Random.insideUnitSphere * patrolRadius;
        lastRandomPos = new Vector3(randomPosition.x, 1.5832f, randomPosition.z);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(RandomPos());

    }

    public void TakeCover()
    {
        nav.speed = 12;
        takeCover = true;
        // set the enemy's destination to the cover point
        nav.SetDestination(coverPoint.position);
    }
}