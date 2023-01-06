using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to be spawned
    public GameObject bossPrefab; // The boss prefab to be spawned (optional)
    public float spawnInterval = 5f; // The interval between each spawn
    public int maxEnemies = 10; // The maximum number of enemies that can be spawned
    public int enemyIncrease = 5; // The number of enemies to increase by each wave
    public int initialWave = 1; // The initial wave number
    public int currentWaveNumber = 1; // The current wave number
    public int bossWave = 10; // The wave at which the boss should be spawned (if bossPrefab is set)
    public bool infiniteWaves = false; // Whether or not the waves should be infinite
    public bool spawnOnStart = false; // Whether or not to spawn enemies on start
    public float spawnRadius = 10f; // The radius within which to spawn the enemies

    private int enemiesSpawned = 0; // The number of enemies that have been spawned in the current wave
    private float timeSinceLastSpawn = 0f; // The time since the last enemy was spawned
    private bool spawning = false; // Whether or not the spawner is currently spawning enemies
    private bool spawningBoss = false; // Whether or not the spawner is currently spawning the boss

    void Start()
    {
        if (spawnOnStart)
        {
            StartSpawning();
        }
    }

    void Update()
    {
        if (spawning)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval && enemiesSpawned < maxEnemies)
            {
                timeSinceLastSpawn = 0f;
                SpawnEnemy();
            }
        }

    }
    int amountOfEnemiesToSpawn = 2;
    void SpawnEnemy()
    {
        GameObject prefabToSpawn;
        if (currentWaveNumber == bossWave && bossPrefab != null)
        {
            prefabToSpawn = bossPrefab;
            spawningBoss = true;
        }
        else
        {
            prefabToSpawn = enemyPrefab;
        }

        // Generate a random position within the spawn radius
        Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = transform.position.y; // Keep the y position the same as the spawner's position


        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(prefabToSpawn, randomPos, transform.rotation);
            enemiesSpawned++;
        }


        if (enemiesSpawned >= maxEnemies && !spawningBoss)
        {
            StopSpawning();
        }
    }

    public void StartSpawning()
    {
        spawning = true;
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    public void NextWave()
    {
        currentWaveNumber++;
        enemiesSpawned = 0;
        maxEnemies += enemyIncrease;
        spawningBoss = false;
        StartSpawning();
    }
}
