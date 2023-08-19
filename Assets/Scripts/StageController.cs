using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageController : MonoBehaviour
{
    enum Stage
    {
        Normal,
        Inverted
    }

    [Header("Normal Assets")]
    [SerializeField] Sprite normalStage;
    [SerializeField] RuntimeAnimatorController normalAnimatorController;

    [Header("Inverted Assets")]
    [SerializeField] Sprite invertedStage;
    [SerializeField] RuntimeAnimatorController invertedAnimatorController;

    [Header("Misc")]
    [SerializeField] GameObject stage;
    [SerializeField] TextMeshProUGUI scoreText;

    private Stage currentStage;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        currentStage = Stage.Normal;
        playerAnimator = FindObjectOfType<PlayerController>().GetComponent<Animator>();
    }

    public void SwitchColors()
    {
        if (currentStage == Stage.Normal)
        {
            SwitchIntoInverted();
        }
        else
        {
            SwitchIntoNormal();
        }
    }

    public string GetCurrentStage()
    {
        return currentStage.ToString();
    }

    private void SwitchIntoNormal()
    {
        // Switch Player
        playerAnimator.runtimeAnimatorController = normalAnimatorController;

        // Switch Stage
        stage.GetComponent<SpriteRenderer>().sprite = normalStage;
        currentStage = Stage.Normal;

        // Change score font color
        scoreText.color = Color.black;
    }

    private void SwitchIntoInverted()
    {
        // Switch Player
        playerAnimator.runtimeAnimatorController = invertedAnimatorController;

        // Switch Stage
        stage.GetComponent<SpriteRenderer>().sprite = invertedStage;
        currentStage = Stage.Inverted;

        // Change score font color
        scoreText.color = Color.white;
    }

    private void SwitchEnemies()
    {
        List<Enemy> currentEnemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        Debug.Log(currentEnemies.Count);
    }
}
