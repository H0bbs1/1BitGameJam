using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] float spawnTimeVariance = 0.25f;
    [SerializeField] float minimumSpawntime = 1.5f;
    [SerializeField] bool isLooping = true;

    private List<Transform> spawnPoints;
    private Transform chosenSpawnPoint;
    private WaveConfigSO currentWave;


    // Spawn Patterns
    /*
     * 1 small enemy
     * 3 small enemies
     * 1 big enemy
     * 1 inverted small enemy
     * 3 inverted small enemies
     * 1 inverted big enemy
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

    public void SpawnEnemy(GameObject enemy)
    {
        int spawningPoint = Random.Range(0, 2);
        Instantiate(enemy, spawnPoints[spawningPoint].position, Quaternion.identity);
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            int enemyToSpawn = Random.Range(0, 6);
            int spawningPoint = Random.Range(0, 2);

            chosenSpawnPoint = spawnPoints[spawningPoint];
            currentWave = waveConfigs[enemyToSpawn];

            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(currentWave.GetEnemyPrefab(i), chosenSpawnPoint.position, Quaternion.identity, transform);
                yield return new WaitForSeconds(0.75f);
            }
            yield return new WaitForSeconds(GetRandomSpawnTime());

        } while (isLooping);
    }

    public void StopSpawner()
    {
        isLooping = false;
        StopCoroutine(SpawnEnemies());
    }

    private float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenWaves - spawnTimeVariance, timeBetweenWaves + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawntime, float.MaxValue);
    }
}
