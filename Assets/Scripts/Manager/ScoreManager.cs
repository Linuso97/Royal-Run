using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreText;

    int score;

    public void IncreaseScore(int amount)
    {
        if (gameManager.GameOver) { return; }

        score += amount;
        scoreText.text = score.ToString();
    }
}
