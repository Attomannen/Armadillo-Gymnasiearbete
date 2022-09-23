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
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate = 10f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 10f;

    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenTarget = Vector3.Distance(transform.position, Target.position);


        if(distanceBetweenTarget <= EnemyRange)
        {
            agent.SetDestination(Target.position);
        }


        if(distanceBetweenTarget <= agent.stoppingDistance && IsAvailable)
        {

                    Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
                    StartCoroutine(StartCooldown());
                    Debug.Log("Bullet");
                
        }
    }
    bool IsAvailable = true;
    [SerializeField] float CooldownDuration = 1.0f;

    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;
    }

}
