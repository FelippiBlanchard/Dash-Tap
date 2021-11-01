using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    [Header("Scores")]
    [SerializeField] private int bestScore;
    [SerializeField] private int currentScore;

    [Header("TextBox")]
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalBestScoreText;



    public static ScoreManager Instance;




    private void Start()
    {
        Instance = this;
    }


    public void AddScore(int value)
    {
        currentScore += value;
        currentScoreText.text = currentScore.ToString();
    }


    public void TrySetBestScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreText.text = bestScore.ToString();
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }


    public void GetBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = bestScore.ToString();
    }


    public void ResetCurrentScore()
    {
        currentScore = 0;
        currentScoreText.text = currentScore.ToString();
    }


    public void SetFinalScores()
    {
        finalBestScoreText.text = bestScore.ToString();
        finalScoreText.text = currentScore.ToString();
    }

}
