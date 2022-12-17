using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField] LayerMask enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1, enemy);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.GetComponentInParent<Arne>() != null)
            {
                hitCollider.gameObject.GetComponentInParent<Arne>().TakeDamage(21);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 20);
    }

}
