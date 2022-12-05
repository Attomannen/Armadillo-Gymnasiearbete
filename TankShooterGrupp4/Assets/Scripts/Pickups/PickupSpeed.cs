using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpeed : MonoBehaviour
{
    [SerializeField] ParticleSystem speedPickUpParticle;
    [SerializeField] float speedBoostAmount;
    private float startingSpeed;
    [SerializeField] float speedDuration;
    [SerializeField] float secondsUntilTopSpeed;

    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] Renderer _renderer;

    PowerupRandomSpawns _randomSpawns;


    private void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) {
            return;
        }
        Debug.Log("Tank hit powerup");

        Debug.Log("Starting speed, should say 20: " + startingSpeed);
        Instantiate(speedPickUpParticle, gameObject.transform);
        startingSpeed = 8;

        _renderer.enabled = false;
        sphereCollider.enabled = false; 
        StartCoroutine(SpeedingTime(other.gameObject));


    }



    IEnumerator SpeedingTime(GameObject collidedObject)
    {

        //float t = 0;
        //t += Time.deltaTime / secondsUntilTopSpeed;
        //collidedObject.GetComponent<Movement>().moveSpeed = Mathf.Lerp(collidedObject.GetComponent<Movement>().moveSpeed, collidedObject.GetComponent<Movement>().moveSpeed + speedBoostAmount, t);
        //Debug.Log("Current movespeed" + collidedObject.GetComponent<Movement>().moveSpeed);

        collidedObject.GetComponent<Movement>().moveSpeed += speedBoostAmount;


        yield return new WaitForSecondsRealtime(speedDuration);
        Debug.Log("Should go back to regular speed");
        collidedObject.GetComponent<Movement>().moveSpeed = startingSpeed;
        gameObject.SetActive(false);
    }

}
