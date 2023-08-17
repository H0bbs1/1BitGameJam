using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject smallEnemyPrefab;
    [SerializeField] GameObject bigEnemyPrefab;
    [SerializeField] bool isLooping = true;

    // Spawn Patterns
    /*
     * 1 = 1 small enemy
     * 2 = 3 small enemies
     * 3 = 1 big enemy
     */

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            int enemyToSpawn = Random.Range(0, 3);

            // Decide enemy to spawn
            if (enemyToSpawn == 0)
            {
                Instantiate(smallEnemyPrefab, transform.position, Quaternion.identity);
            }
            else if (enemyToSpawn == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(smallEnemyPrefab, transform.position, Quaternion.identity);
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
}
