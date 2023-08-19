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
    private ScreenBounds screenBounds;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBounds = FindObjectOfType<ScreenBounds>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DecideDirection();
        rb.velocity = enemyDir * movementSpeed;
    }

    private void FixedUpdate()
    {
        // Screen Wrap
        if (screenBounds.AmIOutOfBounds(transform.position))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(transform.position);
            transform.position = newPosition;
        }
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

    public void SetEnemyDirection(Vector2 dir)
    {
        enemyDir = dir;
    }
}
