using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSightChecker : MonoBehaviour
{
    public SphereCollider Collider;
    [SerializeField] float fieldOfView;
    [SerializeField] LayerMask lineOfSight;

    public delegate void GainSightEvent(Transform target);
    public GainSightEvent onGainSight;
    public delegate void LoseSightEvent(Transform target);
    public GainSightEvent onLoseSight;

    private Coroutine CheckForLineOfSightCoroutine;
    void Start()
    {
        Collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (!CheckLineOfSight(other.transform))
        {
            CheckForLineOfSightCoroutine = StartCoroutine(CheckForLineOfSight(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onLoseSight?.Invoke(other.transform);
        if(CheckForLineOfSightCoroutine != null)
        {
            StopCoroutine(CheckForLineOfSightCoroutine);
        }
    }

    private bool CheckLineOfSight(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward,direction);
        if(dotProduct >= Mathf.Cos(fieldOfView))
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, Collider.radius, lineOfSight))
            {
                onGainSight?.Invoke(target);
                return true;
            }
        }
        return false;
    }

    private IEnumerator CheckForLineOfSight(Transform target)
    {
        WaitForSeconds Wait = new WaitForSeconds(2.5f);

        while (!CheckLineOfSight(target))
        {
            yield return null;
        }
    }
}
