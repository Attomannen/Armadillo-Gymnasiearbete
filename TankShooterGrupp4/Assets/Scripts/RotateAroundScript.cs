using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundScript : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    [SerializeField] GameObject spawnPoint;

    PowerupRandomSpawns powerupRandomSpawns;
    private void Start()
    {
        powerupRandomSpawns = FindObjectOfType<PowerupRandomSpawns>();
    }

    private void Update()
    {
        for (int i = 0; i < powerupRandomSpawns.spawnPoint.Length; i++)
        {
            powerupRandomSpawns.spawnPoint[i].transform.RotateAround(this.gameObject.transform.position, Vector3.up, rotationSpeed);
        }
    }
}
