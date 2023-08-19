using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private List<GameObject> deathCloudPrefabs;
    [SerializeField] private List<GameObject> invertedDeathCloudPrefabs;

    private ScoreSystem scoreSystem;
    private StageController stageController;

    private void Awake()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>();
        stageController = FindObjectOfType<StageController>();
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

            // Display death poof msg
            GameObject deathCloud;
            string currentStage = stageController.GetCurrentStage();

            if (currentStage == "Normal")
            {
                deathCloud = deathCloudPrefabs[Random.Range(0, 2)];
            }
            else
            {
                deathCloud = invertedDeathCloudPrefabs[Random.Range(0, 2)];
            }
            GameObject deathCloudInstance = Instantiate(deathCloud, collision.transform.position, Quaternion.identity);
            Destroy(deathCloudInstance, 0.25f);
            Destroy(collision.gameObject);
        }
    }
}
