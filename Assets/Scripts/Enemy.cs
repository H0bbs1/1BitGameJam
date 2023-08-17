using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float movementSpeed = 1f;

    //Components
    Rigidbody2D rb;

    private Vector2 enemyDir = Vector2.right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = enemyDir * movementSpeed;
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
