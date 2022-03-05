using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [Header("Scores")]
    [SerializeField] private int bestScore;
    [SerializeField] private int currentScore;

    [Header("Boost Score")]
    [SerializeField] private bool boostActivated;
    [SerializeField] private MultiplicationBoost[] multiplicationBoosts;
    [SerializeField] private TextMeshProUGUI multBoostText;
    [SerializeField] private Slider sliderBoost;
    private int currentIndexMultBoost;
    private float timerBoost;
    private int currentSequenceKills;
    private bool countingBoost;


    [Header("TextBox")]
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalBestScoreText;

    [Header("Events")]
    [SerializeField] private UnityEvent<int> onAddScore;
    [SerializeField] private UnityEvent onChangeMultiplicationBoost;
    [SerializeField] private UnityEvent onResetMultiplicationBoost;
    [SerializeField] private UnityEvent whenNewBestScore; 
    [SerializeField] private UnityEvent whenNotNewBestScore;



    public static ScoreManager Instance;




    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (boostActivated && countingBoost)
        {
            if (timerBoost > 0)
            {
                timerBoost -= Time.deltaTime;
                sliderBoost.value = timerBoost / multiplicationBoosts[currentIndexMultBoost].intervalToBoost;
            }
            else
            {
                ResetMultiplicationBoost();
            }
        }
    }

    public void InitializeScore()
    {
        ResetCurrentScore();
        GetBestScore();
    }

    public void InitializeFinalScore()
    {
        TrySetBestScore();
        SetFinalScores();
    }


    public void AddScore(int value)
    {
        if (boostActivated)
        {
            value = MultiplicateScore(value);
        }
        onAddScore.Invoke(value);
        currentScore += value;
        currentScoreText.text = currentScore.ToString();

    }

    private int MultiplicateScore(int value)
    {
        if (timerBoost > 0)
        {
            if(currentSequenceKills > multiplicationBoosts[currentIndexMultBoost].targetsToReach)
                if (currentIndexMultBoost < multiplicationBoosts.Length - 1)
                    ChangeMultiplicationBoost();

            value *= multiplicationBoosts[currentIndexMultBoost].multBoost;
        
        }

        timerBoost = multiplicationBoosts[currentIndexMultBoost].intervalToBoost;
        countingBoost = true;
        currentSequenceKills++;
        return value;
    }

    private void ResetMultiplicationBoost()
    {
        if(currentIndexMultBoost>0)
            onResetMultiplicationBoost.Invoke();
        currentSequenceKills = 0;
        currentIndexMultBoost = 0;
        countingBoost = false;
    }
    private void ChangeMultiplicationBoost()
    {
        currentIndexMultBoost++;
        currentSequenceKills = 0;

        var multValue = multiplicationBoosts[currentIndexMultBoost].multBoost;
        multBoostText.text = "x" + multValue.ToString();
        multBoostText.color = multiplicationBoosts[currentIndexMultBoost].color;
        sliderBoost.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = multBoostText.color;
        onChangeMultiplicationBoost.Invoke();
    }

    public void TrySetBestScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreText.text = bestScore.ToString();
            PlayerPrefs.SetInt("BestScore", bestScore);

            whenNewBestScore.Invoke();

        }
        else
        {
            whenNotNewBestScore.Invoke();
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

[Serializable]
public class MultiplicationBoost
{
    public int multBoost;
    public int targetsToReach;
    public float intervalToBoost;
    public Color color;
}
