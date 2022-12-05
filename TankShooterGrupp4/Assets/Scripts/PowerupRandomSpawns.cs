using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupRandomSpawns : MonoBehaviour
{
    public Transform[] spawnPoint;

    [SerializeField] float timeBetweenSpawns;

    [SerializeField] GameObject[] spawnablePowerupsArray;

    [SerializeField] GameObject pickupsHolder;

    void Update()
    {
        StartCoroutine(SpawnPowerup());

    }

     public IEnumerator SpawnPowerup()
    {
        yield return new WaitForSecondsRealtime(timeBetweenSpawns);
        Instantiate(spawnablePowerupsArray[Random.Range(0, spawnablePowerupsArray.Length)], spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity, pickupsHolder.transform);
        StopAllCoroutines();
    }


}
