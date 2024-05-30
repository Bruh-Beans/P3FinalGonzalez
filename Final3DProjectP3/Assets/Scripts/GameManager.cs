using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winOverText;
    public TextMeshProUGUI highScoreText; // Add this reference in the inspector
    public Button restartButton;
    public Button startButton;
    public GameObject titleScreen;
    public bool isGameActive;
    public TextMeshProUGUI livesText;
    public int lives;

    void Start()
    {
    }

    void Update()
    {
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void Win()
    {
        startButton.gameObject.SetActive(true);
        winOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        DetectCollision.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartAgain()
    {
        DetectCollision.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        lives++;
        UpdateLives();
    }

    public void UpdateLives()
    {
        if (isGameActive)
        {
            lives--;
            livesText.text = "Lives: " + lives;
            if (lives == 0)
            {
                GameOver();
            }
        }
    }
}
