using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyShootAI : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] Transform Target;
    [SerializeField] float EnemyRange = 20f;
    float distanceBetweenTarget;
    [SerializeField] Transform[] projectileSpawnPoint;
    [SerializeField] GameObject projectilePrefab;
    float countDownFire = 0f;
    [SerializeField] float fireRate = 10f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenTarget = Vector3.Distance(transform.position, Target.position);


        if(distanceBetweenTarget <= EnemyRange)
        {
            agent.SetDestination(Target.position);
        }


        if(distanceBetweenTarget <= agent.stoppingDistance)
        {
            if (countDownFire <= 0f)
            {


                foreach (Transform SpawnPoints in projectileSpawnPoint)
                {

                    Instantiate(projectilePrefab, SpawnPoints.position, transform.rotation);
                    Debug.Log("Bullet");
                }


            }

            countDownFire = 1f / fireRate;

            countDownFire -= Time.deltaTime;
        }
    }
}
