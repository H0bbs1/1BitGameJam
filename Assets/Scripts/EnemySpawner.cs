using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject smallEnemyPrefab;
    [SerializeField] GameObject bigEnemyPrefab;
    [SerializeField] bool isLooping = true;

    private List<Transform> spawnPoints;
    private Transform chosenSpawnPoint;


    // Spawn Patterns
    /*
     * 1 = 1 small enemy
     * 2 = 3 small enemies
     * 3 = 1 big enemy
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
            int enemyToSpawn = Random.Range(0, 3);
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
            else
            {
                Instantiate(bigEnemyPrefab, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(4f);

        } while (isLooping);
    }

    public void StopSpawner()
    {
        StopCoroutine(SpawnEnemies());
    }
}
