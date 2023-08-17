using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float movementSpeed = 1f;

    //Components
    Rigidbody2D rb;

    private Vector2 enemyDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DecideDirection();
        rb.velocity = enemyDir * movementSpeed;
    }

    private void DecideDirection()
    {
        int enemyDirRandom = Random.Range(0, 2);
        if (enemyDirRandom == 0)
        {
            enemyDir = Vector2.right;
        }
        else
        {
            enemyDir = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Hit player");
        }
    }

    public void SetEnemyDirection(Vector2 dir)
    {
        enemyDir = dir;
    }
}
