using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private ScoreSystem scoreSystem;

    private void Awake()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            if (collision.gameObject.tag == "Small_Enemy")
            {
                scoreSystem.IncrementScoreSmall();
            }
            else if (collision.gameObject.tag == "Big_Enemy")
            {
                scoreSystem.IncrementScoreBig();
            }
            Destroy(collision.gameObject);
        }
    }
}
