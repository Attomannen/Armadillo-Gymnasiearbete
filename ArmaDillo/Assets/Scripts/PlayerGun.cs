using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static Unity.VisualScripting.Member;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] int maxMagSize = 17;
    AudioSource source;
    [SerializeField] int allAmmo = 102;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] TextMeshProUGUI magText;

    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ShootingSystem;
    [SerializeField]
    private Transform BulletSpawnPoint;
    [SerializeField]
    private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private float ShootDelay = 0.5f;
    [SerializeField]
    private LayerMask Mask;
    [SerializeField]
    private float BulletSpeed = 100;
    int magazine;
    PlayerInput input;
    InputAction reload;

    [SerializeField] Animator anim;
    private float LastShootTime;
    private void Start()
    {
        input = GetComponent<PlayerInput>();
        magazine = maxMagSize;
        source = GetComponent<AudioSource>();
        reload = input.actions["Reload"];


    }

    private void Update()
    {
        magText.text = magazine + "/" + allAmmo;
        if (magazine == 0 && !startReloading && allAmmo !> 0 || reload.triggered && magazine <= 16 && !startReloading && allAmmo! > 0)
        {
            startReloading = true;
            StartCoroutine(Reload());
        }
    }
    int holdAmountOfShots;

    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time && !isReloading)
        {
            holdAmountOfShots++;
            magazine--;
            source.PlayOneShot(shootSound);

            anim.SetTrigger("Recoil");
            ShootingSystem.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(Camera.main.transform.position, direction, out RaycastHit hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                LastShootTime = Time.time;
            }
            // this has been updated to fix a commonly reported problem that you cannot fire if you would not hit anything
            else
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + GetDirection() * 100, Vector3.zero, false));

                LastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = Camera.main.transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z));

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {

        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }

    bool startReloading = false;

    bool isReloading;
    bool reloading;
    float reloadTime = 1.5f;
    public IEnumerator Reload()
    {
        isReloading = true;
        reloading = false;
        yield return new WaitForSeconds(0.15f);

        source.PlayOneShot(reloadSound);

        anim.SetTrigger("Reload");

        yield return new WaitForSeconds(reloadTime);
        if (!reloading)
        {
            allAmmo -= holdAmountOfShots;
            magazine = maxMagSize;
            reloading = true;
            holdAmountOfShots = 0;
        }
        startReloading = false;
        isReloading = false;
        StopCoroutine(Reload());
    }
}
