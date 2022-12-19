using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Pickup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI teethText;
    [SerializeField] LayerMask teethLayer;
    int collectedTeeth;

    private void Update()
    {
        teethText.text = "Teeth: " + collectedTeeth;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2, teethLayer);
        foreach (var hitCollider in hitColliders)
        {
            collectedTeeth++;

            Destroy(hitCollider.gameObject);
        }
    }



}
