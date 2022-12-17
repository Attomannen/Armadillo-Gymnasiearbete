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
    InputAction reloadAction;

    PlayerInput playerInput;
    Camera cam;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform shootPointTransform;
    [SerializeField] float bulletHitMissDistance = 25f;
    [SerializeField] LayerMask bulletMarkMask;

    [SerializeField] GameObject bulletMark;

    [SerializeField] float force = 100f;

    [SerializeField] int maxMagSize = 17;

    int magazine;
    [SerializeField] TextMeshProUGUI magText;

    [SerializeField] Animator pistolAnim;
    AudioSource source;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField]
    private TrailRenderer BulletTrail;
    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        magText.text = magazine + "";
        magazine = maxMagSize;
        cam = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Fire"];
        reloadAction = playerInput.actions["Reload"];
        Cursor.lockState = CursorLockMode.Locked;

    }

    bool IsAvailable = true;
    [SerializeField] float CooldownDuration = 0f;
    bool startReloading = false;
    private void Update()
    {

        magText.text = magazine + "";

        if (shootAction.triggered && IsAvailable && magazine != 0 && !isReloading)
        {
            ShootGun();
            source.PlayOneShot(shootSound);
            StartCoroutine(StartCooldown());
        }
        if (magazine == 0 && !startReloading || reloadAction.triggered && magazine <= 16 && !startReloading)
        {
            startReloading = true;
            StartCoroutine(Reload());
        }
    }
    bool isReloading;
    bool reloading;
    float reloadTime = 1.5f;
    public IEnumerator Reload()
    {
        isReloading = true;
        reloading = false;
        yield return new WaitForSeconds(0.15f);

        source.PlayOneShot(reloadSound);

        pistolAnim.SetTrigger("Reload");

        yield return new WaitForSeconds(reloadTime);
        if (!reloading)
        {
            magazine = maxMagSize;
            reloading = true;
        }
        startReloading = false;
        isReloading = false;
        StopCoroutine(Reload());
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;

    }
    [SerializeField] GameObject bullet;
    // Update is called once per frame

    void ShootGun()
    {
        pistolAnim.SetTrigger("Recoil");
        magazine--;
        RaycastHit hit;
        Debug.Log(magazine);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, bulletHitMissDistance, layerMask))
        {

            Instantiate(bullet, shootPointTransform.position, Quaternion.identity);

            #region placeHolder
            //Instantiate(bulletMark, hit.point + hit.normal * 0.001f, Quaternion.identity);

            //if (hit.collider.gameObject.tag != "Player")
            //{
            //    if (hit.collider.gameObject.GetComponent<MeshRenderer>() != null)
            //    {
            //        hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            //    }
            //    Debug.Log(hit.transform.name);

            //    if (hit.rigidbody != null)
            //    {
            //        hit.rigidbody.AddForce(-hit.normal * force);
            //    }
            //    if (hit.collider.gameObject.tag == "Enemy")
            //    {
            //        hit.transform.gameObject.GetComponentInParent<Arne>().TakeDamage(25);
            //    }
            //}
            #endregion
        }
    }
}
