using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TailRaycastFix : MonoBehaviour
{
    [SerializeField] LayerMask ground;

    [SerializeField] Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AlignTailToGround();
    }


    void AlignTailToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.transform.position, -transform.forward, out hit, 1, ground))
        {
            transform.position = hit.point + offset;
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.forward);
    }
}
