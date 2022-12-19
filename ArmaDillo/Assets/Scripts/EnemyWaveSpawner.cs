using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] float spawnInterval = 1.0f;

    [SerializeField] int enemiesPerWave = 10;

    [SerializeField] int maxWaves = 10;

    [SerializeField] int increaseWave = 5;

    [SerializeField] int additionalEnemiesPerWave = 5;

    [SerializeField] List<Transform> spawnPoints;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentWave < maxWaves)
        {
            int enemiesToSpawn = enemiesPerWave;
            if (currentWave >= increaseWave)
            {
                enemiesToSpawn += additionalEnemiesPerWave;
            }

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }

            currentWave++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
