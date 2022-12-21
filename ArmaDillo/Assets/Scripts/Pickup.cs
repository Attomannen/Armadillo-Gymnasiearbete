using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.ShaderGraph.Internal;

public class Pickup : MonoBehaviour
{
    public enum pickUpType { health, teeth };
    public pickUpType activePickUp;
    [SerializeField] float pickUpRange;
    [Header( "Teeth Pick Up")]
    [SerializeField] TextMeshProUGUI teethText;
    [SerializeField] LayerMask teethLayer;

    [Header("Health Pick Up")]
    [SerializeField] LayerMask healthLayer;
    int collectedTeeth;

    private void Update()
    {
        TeethPickUp();
        HealthPickUp();
    }

    [SerializeField] Vector3 offset;
    void TeethPickUp()
    {
        if(activePickUp == pickUpType.teeth)
        {
            teethText.text = "Teeth: " + collectedTeeth;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + offset, pickUpRange, teethLayer);
            foreach (var hitCollider in hitColliders)
            {
                collectedTeeth++;

                Destroy(gameObject);
            }
        }
        else
        {
            return;
        }
    }

    void HealthPickUp()
    {
        if (activePickUp == pickUpType.health)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + offset, pickUpRange, healthLayer);
            foreach (var hitCollider in hitColliders)
            {
                Debug.Log("Meow");
                PlayerHealth health = hitCollider.gameObject.GetComponent<PlayerHealth>();
                health.TakeDamage(-50);
                Destroy(gameObject);
            }
        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + offset, pickUpRange);
    }
}
