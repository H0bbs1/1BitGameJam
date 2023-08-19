using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Big_Enemy" || collision.gameObject.tag == "Small_Enemy")
        {
            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
            enemySpawner.SpawnEnemy(collision.gameObject);
        }
    }
}
