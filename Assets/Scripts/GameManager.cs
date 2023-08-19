using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController playerController;
    LoseCanvas loseCanvas;

    private void Awake()
    {
        loseCanvas = FindObjectOfType<LoseCanvas>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        loseCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerController.isAlive)
        {
            Invoke("ShowLoseCanvas", 2.0f);
        }
    }

    void ShowLoseCanvas()
    {
        loseCanvas.gameObject.SetActive(true);
    }
}
