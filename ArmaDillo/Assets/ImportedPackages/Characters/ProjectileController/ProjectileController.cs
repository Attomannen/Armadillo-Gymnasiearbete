using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour 
{

    public enum ProjectileType {stupid, intelligent}; 
    public ProjectileType MissileType; 

    public GameObject Explosion; 
    public bool LookAt; 
    [HideInInspector] public Vector3 Direction; 

    // For following missiles 

     Transform Target; 
     public float RotationSpeed; 
     public float TranslationSpeed; 
     public float LifeTime = 1f;
    Vector3 to_Targets;
    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        to_Targets = (Target.position - transform.position).normalized;
    }
    
    void Update()
    {
        if(MissileType == ProjectileType.stupid)
        {
            transform.position += (to_Targets + Direction*Time.deltaTime)/2 * TranslationSpeed; 
        }
        else
        {
            Vector3 to_target = (Target.position - transform.position).normalized;
            transform.forward = Vector3.RotateTowards(transform.forward, to_target, RotationSpeed*Time.deltaTime, 0f); 
            transform.position += transform.forward*TranslationSpeed*Time.deltaTime; 

            LifeTime -= Time.deltaTime; 
            if(LifeTime < 0f)
            {
                Debug.Log("Life over"); 
                EngageDestruction(); 
            }
        }
    }

    public void SetDirection(Vector3 v)
    {
        Direction = v; 
        if(LookAt)
        transform.LookAt(transform.position + 10f*Direction); 
    }

    public void SetMissileParameters(Transform t, float s, float rs, float lt)
    {
        Target = t; 
        RotationSpeed = rs;
        TranslationSpeed = s; 
        LifeTime = lt; 

    }

    void EngageDestruction()
    {
        GameObject explo = Instantiate(Explosion, transform.position, Quaternion.identity) as GameObject; 
        Destroy(explo, 3f); 
        Destroy(gameObject);     
    }

    void OnTriggerEnter(Collider other)
    {
        EngageDestruction(); 
        if(other.gameObject.transform == Target)
        {
            Target.GetComponent<PlayerHealth>().TakeDamage(12f);
        }
    }
}