using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreIncreaseAmount = 100;
    
    float currentScore = 0;
    
    public void IncreaseScore()
    {
        if (gameManager.GameOver) return;
        
        currentScore = currentScore + scoreIncreaseAmount;
        scoreText.text = currentScore.ToString();
    }
}
