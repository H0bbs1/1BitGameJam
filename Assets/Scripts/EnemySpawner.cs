using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject smallEnemyPrefab;
    [SerializeField] GameObject bigEnemyPrefab;
    [SerializeField] GameObject invertedSmallEnemyPrefab;
    [SerializeField] GameObject invertedBigEnemyPrefab;
    [SerializeField] bool isLooping = true;

    private List<Transform> spawnPoints;
    private Transform chosenSpawnPoint;


    // Spawn Patterns
    /*
     * 1 = 1 small enemy
     * 2 = 3 small enemies
     * 3 = 1 big enemy
     * 4 = 1 inverted small enemy
     * 5 = 3 inverted small enemies
     * 6 = 1 inverted big enemy
     */

    private void Start()
    {
        spawnPoints = new List<Transform>();
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            int enemyToSpawn = Random.Range(0, 6);
            int spawningPoint = Random.Range(0, 2);

            chosenSpawnPoint = spawnPoints[spawningPoint];

            // Decide enemy to spawn
            if (enemyToSpawn == 0)
            {
                Instantiate(smallEnemyPrefab, chosenSpawnPoint.position, Quaternion.identity);
            }
            else if (enemyToSpawn == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(smallEnemyPrefab, chosenSpawnPoint.position, Quaternion.identity);
                    yield return new WaitForSeconds(0.75f);
                }
            }
            else if (enemyToSpawn == 2)
            {
                Instantiate(bigEnemyPrefab, chosenSpawnPoint.position, Quaternion.identity);
            }
            else if (enemyToSpawn == 4)
            {
                Instantiate(invertedSmallEnemyPrefab, chosenSpawnPoint.position, Quaternion.identity);
            }
            else if (enemyToSpawn == 5)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(invertedSmallEnemyPrefab, chosenSpawnPoint.position, Quaternion.identity);
                    yield return new WaitForSeconds(0.75f);
                }
            }
            else
            {
                Instantiate(invertedBigEnemyPrefab, chosenSpawnPoint.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(3.5f);

        } while (isLooping);
    }

    public void StopSpawner()
    {
        StopCoroutine(SpawnEnemies());
    }
}
