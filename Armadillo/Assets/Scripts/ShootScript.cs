using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using UnityEngine.UI;
using TMPro;

public class ShootScript : MonoBehaviour
{
    InputAction shootAction;
    PlayerInput playerInput;
    Camera cam;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPointTransform;
    [SerializeField] float bulletHitMissDistance = 25f;

    [SerializeField] GameObject bulletMark;

    [SerializeField] float force = 100f;

    [SerializeField] int maxMagSize = 21;

    [SerializeField] int maxAmmo;
    int magazine;
    [SerializeField] TextMeshProUGUI magText;
    // Start is called before the first frame update
    void Awake()
    {
        magText.text = magazine + "/" + maxAmmo;
        magazine = maxMagSize;
        health = GetComponent<PlayerHealth>();
        cam = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Fire"];

        Cursor.lockState = CursorLockMode.Locked;

    }

    bool IsAvailable = true;
    [SerializeField] float CooldownDuration = 0f;
    private void Update()
    {

        float action = shootAction.ReadValue<float>();
        magText.text = magazine + "/" + maxAmmo;

        if (action == 1 && IsAvailable && magazine != 0)
        {
            ShootGun();
            StartCoroutine(StartCooldown());
        }
        if(magazine == 0)
        {
            StartCoroutine(Reload());
        }
    }

    int reloadTime = 2;
    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        magazine = maxMagSize;
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;
    }
    PlayerHealth health;
    // Update is called once per frame
    void ShootGun()
    {
        magazine--;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, bulletHitMissDistance))
        {

            if (hit.collider.gameObject.tag != "Player")
            {
                if (hit.collider.gameObject.GetComponent<MeshRenderer>() != null)
                {
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                }
                Debug.Log(hit.transform.name);

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * force);
                }
                var bulletHole = Instantiate(bulletMark, hit.point + hit.normal * 0.001f, Quaternion.identity);
                bulletHole.transform.LookAt(hit.point + hit.normal * 1f);
                bulletHole.transform.parent = hit.transform;
                Destroy(bulletHole, 6);
                if (hit.collider.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(20);
                }
            }

        }
    }
}
