using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectCollision : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private static int score = 0;
    public static int lives = 1;
    private bool alive = true;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    public ParticleSystem explosionParticle;
    private const string HighScoreKey = "HighScore";

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        highScoreText = GameObject.FindWithTag("HighScoreText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateHighScoreDisplay();
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && alive)
        {
            Destroy(gameObject);
            lives -= 1;
            Debug.Log("Lives: " + lives);
            if (lives <= 0)
            {
                gameManager.GameOver();
                alive = false;
                Debug.Log("GameOver");
                Destroy(gameObject);
                Destroy(other.gameObject);
                CheckAndSetHighScore();
            }
        }
        else if (other.CompareTag("Bone"))
        {
            Destroy(gameObject);
            score += 1;
            UpdateScore(0);
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void CheckAndSetHighScore()
    {
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.Save();
        }
        UpdateHighScoreDisplay();
    }

    private void UpdateHighScoreDisplay()
    {
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    public static void ResetScore()
    {
        score = 0;
    }
}
