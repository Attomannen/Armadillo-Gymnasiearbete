using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TeethPickUp : MonoBehaviour
{
    [Header("Teeth Pick Up")]
    [SerializeField] TextMeshProUGUI teethText;
    [SerializeField] LayerMask teethLayer;
    [SerializeField] float pickUpRange;

    [SerializeField] Vector3 offset;

    int collectedTeeth;


    private void Update()
    {
        PickUp();
    }


    void PickUp()
    {

            teethText.text = "Teeth: " + collectedTeeth;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + offset, pickUpRange, teethLayer);
            foreach (var hitCollider in hitColliders)
            {
                collectedTeeth++;

                Destroy(hitCollider.gameObject);
            }

    }

}
