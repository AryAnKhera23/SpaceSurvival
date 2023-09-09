using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreMultiplier;

    public const string HighScoreKey = "HighScore";
    private float score;
    private bool incrementScore = true;
    

    void Update()
    {
        if (!incrementScore) { return; }
        score += Time.deltaTime * scoreMultiplier;
        
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int FinalScore()
    {
        incrementScore = false;
        scoreText.text = string.Empty;
        return Mathf.FloorToInt(score);
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }

    public void StartTimer()
    {
        incrementScore = true;
    }
}
