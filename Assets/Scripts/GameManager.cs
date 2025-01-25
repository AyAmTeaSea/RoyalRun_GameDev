using System;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool gameOver = false;
    
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    private void Start()
    {
        timeLeft = startTime;
    }

    void Update()
    {
        DecreaseTime();
    }

    public void IncreaseTime(float timeIncrease)
    {
        timeLeft += timeIncrease;
    }
    
    private void DecreaseTime()
    {
        if (GameOver) return;
        
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("0.0");

        if (timeLeft <= 0f)
        {
            PlayerGameOver();
        }
    }

    private void PlayerGameOver()
    {
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
        playerController.enabled = false;
        GameOver = true;
    }
}
