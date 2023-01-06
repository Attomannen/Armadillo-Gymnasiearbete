
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Arne : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [SerializeField] Animator anim;
    float normalRange, normalAttackRange;
    private void Awake()
    {
        normalAttackRange = attackRange;
        normalRange = sightRange;
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange && !dead) Patroling();
        if (playerInSightRange && !playerInAttackRange && !dead) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !dead) AttackPlayer();

        if (!playerInAttackRange && playerInSightRange && !dead)
        {

            anim.SetBool("isWalking", true);
            anim.ResetTrigger("Attack");

        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2.5f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.Normalize();

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotationSpeed/2 * Time.deltaTime);
    }
   [SerializeField] float rotationSpeed;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;

    [SerializeField] Transform arneMouth;
    Vector3 aim_position;
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        Vector3 lookDirection = player.position - transform.position;
        lookDirection.Normalize();
        anim.SetBool("isWalking", false);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
        if (!alreadyAttacked)
        {
            anim.SetTrigger("Attack");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    void SpawnBullet()
    {
        if (playerInAttackRange)
        {
        Instantiate(bullet, arneMouth.position, Quaternion.identity);
        }
        anim.ResetTrigger("Attack");

    }


    void CheckAttackRange()
    {
        anim.ResetTrigger("Attack");

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 1) Invoke(nameof(DestroyEnemy), 0f);

        StartCoroutine(PlayerAttackedOutSideRange());
    }

    IEnumerator PlayerAttackedOutSideRange()
    {

        attackRange = 950;
        sightRange = 950;
        yield return new WaitForSeconds(2.7f);
        attackRange = normalRange;
        sightRange = normalRange;
        StopCoroutine(PlayerAttackedOutSideRange());
    }
    [SerializeField] GameObject toothObject;
    public void spawnTooth()
    {
        for(int i = 0; i < 1; i++)
        {
         for(int b = 0; b < Random.Range(0,3); b++)
            {
             Instantiate(toothObject,transform.position, Quaternion.identity);
            }
            Instantiate(toothObject, transform.position, Quaternion.identity);

        }

    }

    [SerializeField] LayerMask playerBullet;
  
    bool dead = false;
    private void DestroyEnemy()
    {
        if (!dead)
        {
        anim.SetTrigger("Death");
        dead = true; 
        }
        agent.isStopped = true;
    }
    [SerializeField] GameObject deathSmoke;
  public void spawnDeathSmoke()
    {
        Instantiate(deathSmoke, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
