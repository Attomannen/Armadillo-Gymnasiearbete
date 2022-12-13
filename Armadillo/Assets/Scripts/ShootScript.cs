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

    [SerializeField] int maxMagSize = 17;
    
    int magazine;
    [SerializeField] TextMeshProUGUI magText;

    [SerializeField] Animator pistolAnim;
    AudioSource source;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        magText.text = magazine + "";
        magazine = maxMagSize;
        cam = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Fire"];

        Cursor.lockState = CursorLockMode.Locked;

    }

    bool IsAvailable = true;
    [SerializeField] float CooldownDuration = 0f;
    private void Update()
    {

        magText.text = magazine + "";

        if (shootAction.triggered && IsAvailable && magazine != 0)
        {
            ShootGun();
            source.PlayOneShot(shootSound);
            StartCoroutine(StartCooldown());
        }
        if (magazine == 0)
        {
            StartCoroutine(Reload());
        }
    }
    bool isPlayingReloadAnim;
    bool reloading;
    float reloadTime = 1.5f;
    public IEnumerator Reload()
    {
        reloading = false;
        yield return new WaitForSeconds(0.15f);
        if (!isPlayingReloadAnim && magazine == 0)
        {
        Debug.Log("IsPlayingAnimation");
        pistolAnim.SetTrigger("Reload");
            source.PlayOneShot(reloadSound);
        }
        isPlayingReloadAnim = true;
        yield return new WaitForSeconds(reloadTime);
        isPlayingReloadAnim = false;
        if (!reloading)
        {
        magazine = maxMagSize;
            reloading = true;
        }
        StopCoroutine(Reload());
    }
    bool animShoot;
    public IEnumerator StartCooldown()
    {
        animShoot = true;
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;

    }
    // Update is called once per frame
    void ShootGun()
    {
        pistolAnim.SetTrigger("Recoil");
        magazine--;
        RaycastHit hit;
        Debug.Log(magazine);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, bulletHitMissDistance, layerMask))
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
                if (hit.collider.gameObject.GetComponent<EnemyAI>() != null)
                {
                    //hit.collider.gameObject.GetComponent<EnemyAI>().TakeCover();
                }
            }

        }
    }
}
