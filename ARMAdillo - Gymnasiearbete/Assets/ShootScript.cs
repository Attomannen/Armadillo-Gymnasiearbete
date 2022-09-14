using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShootScript : MonoBehaviour
{
    InputAction shootAction;
    PlayerInput playerInput;
    Camera cam;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPointTransform;
    [SerializeField] Transform bulletParent;
    [SerializeField] float bulletHitMissDistance = 25f;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Fire"];

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
    }

    // Update is called once per frame
    void ShootGun()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, shootPointTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity))
        {


            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = cam.transform.position + cam.transform.forward * bulletHitMissDistance;
            bulletController.hit = false;
        }
    }
}
