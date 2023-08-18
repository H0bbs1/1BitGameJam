using System.Collections;
using System.Collections.Generic;
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

    private Stage currentStage;
    private List<Enemy> enemyList;
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

    private void SwitchIntoNormal()
    {
        // Switch Player
        playerAnimator.runtimeAnimatorController = normalAnimatorController;

        // Switch Enemies
        List<Enemy> currentEnemiesList = new List<Enemy>(FindObjectsOfType<Enemy>());

        // Switch Stage
        stage.GetComponent<SpriteRenderer>().sprite = normalStage;
        currentStage = Stage.Normal;
    }

    private void SwitchIntoInverted()
    {
        // Switch Player
        playerAnimator.runtimeAnimatorController = invertedAnimatorController;

        // Switch Enemies

        // Switch Stage
        stage.GetComponent<SpriteRenderer>().sprite = invertedStage;
        currentStage = Stage.Inverted;
    }

    public void SwitchPlayer()
    {
        
    }

    private void SwitchEnemies()
    {
        List<Enemy> currentEnemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        Debug.Log(currentEnemies.Count);
    }
}
