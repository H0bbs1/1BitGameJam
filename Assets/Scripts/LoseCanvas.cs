using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCanvas : MonoBehaviour
{
    [SerializeField] private GameObject normalLostMessage;
    [SerializeField] private GameObject invertedLostMessage;

    StageController stageController;

    private void Awake()
    {
        stageController = FindObjectOfType<StageController>();
    }

    private void OnEnable()
    {
        string currentStage = stageController.GetCurrentStage();
        if (currentStage == "Normal")
        {
            normalLostMessage.SetActive(false);
            invertedLostMessage.SetActive(true);
        }
        else
        {
            invertedLostMessage.SetActive(false);
            normalLostMessage.SetActive(true);
        }
    }

    public void OnClickPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
