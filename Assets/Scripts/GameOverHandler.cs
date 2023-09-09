using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpawnner asteroidSpawnner;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private SoundManager soundManager;
    public void EndGame()
    {
        asteroidSpawnner.enabled = false;

        int finalScore = scoreSystem.FinalScore();
        gameOverText.text = $"Your Score: {finalScore}";
        gameOverDisplay.gameObject.SetActive(true);
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
        highScoreText.text = $"Previous High Score: {highScore}";
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueButton()
    {
        AdManager.Instance.ShowAd(this);
        continueButton.interactable = false;
    }
    public void Continue()
    {
        scoreSystem.StartTimer();

        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        asteroidSpawnner.enabled = true;
        gameOverDisplay.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
