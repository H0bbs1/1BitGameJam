using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void IncrementScoreSmall()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void IncrementScoreBig()
    {
        score += 2;
        scoreText.text = score.ToString();
    }
}
